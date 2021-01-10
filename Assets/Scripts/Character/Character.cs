using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, Interactable
{
    public virtual int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health > MaxHealth)
            {
                _health = MaxHealth;
            }

            if (_health < 0)
            {
                _health = 0;
            }
        }
    }
    protected Rigidbody rb;
    public int MaxHealth { get; protected set; }
    public int Damage;
    public float AttackSpeed;
    public WornItemsGroup WornItems;
    protected float TimeBetweenAttacks
    {
        get
        {
            // base is 1s, so if attackSpeed = 100%, timeBetweenAttacks == 1. if it's 200% it's .5s. 400% == .25s
            return 1 / AttackSpeed;
        }
    }
    protected bool CanAttackWhileMoving => false;
    public float MeleeAttackRange;
    public float RangedAttackRange;
    public Skill PrimarySkill;
    public Skill SecondarySkill;
    public Character Target;
    public string Name;
    public Allegiance Allegiance;
    public HashSet<Allegiance> Enemies;
    public Body Body;
    public int MaxMana;
    public virtual int Level { get; set; }
    private int _mana;
    public virtual int Mana
    {
        get { return _mana; }
        set
        {
            _mana = value;
            if (_mana > MaxMana)
            {
                _mana = MaxMana;
            }

            if (_mana < 0)
            {
                _mana = 0;
            }
        }
    }
    public abstract float ManaRegenPerMinute { get; }

    private HashSet<PassiveSkill> _passiveSkills;
    public HashSet<PassiveSkill> PassiveSkills
    {
        get
        {
            if (_passiveSkills == null)
            {
                _passiveSkills = new HashSet<PassiveSkill>();
            }

            return _passiveSkills;
        }
        set { _passiveSkills = value; }
    }

    protected Healthbar Healthbar;
    protected bool IsDead;
    protected int _health;
    public float MovementSpeed;
    public AnimationState _animationState;
    public AnimationState CurrentAnimation
    {
        get
        {
            return _animationState;
        }
        set
        {
            _animationState = value;
            this.Body.Animator.SetInteger("Animation_State", (int)_animationState);
        }
    }

    public virtual void Initialize()
    {
        this.Healthbar = Instantiate(Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/Healthbar"), Vector3.zero,
            new Quaternion(), Constants.Persistant.HealthUIParent).GetComponent<Healthbar>();
        this.Healthbar.SetOwner(this.transform);
        this.Body = new Body(this.transform.Find("Body"));
        this.IsDead = false;
        this.WornItems = new WornItemsGroup("Equipped Items", this);
        this.SetInitialStats();
        this.Health = this.MaxHealth;
        this.Mana = this.MaxMana;
        this.initialRotation = Body.Transform.rotation;
        this.rb = this.GetComponent<Rigidbody>();
        this.rb.constraints =
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotation;


    }

    void Awake()
    {
        Initialize();
    }

    protected virtual void UpdateLoop()
    {
        CheckForTarget();
        RegenerateMana();
        ApplyPassiveEffects();
    }

    void Update()
    {
        UpdateLoop();
    }

    public virtual void PrimaryAttack()
    {
        PerformAttack(PrimarySkill);
    }

    /// <summary>
    /// Tries to attack the target with the given skill. Returns true if successful, false otherwise.
    /// </summary>
    protected bool PerformAttack(Skill skill)
    {
        if (IsDead || Target == null)
        {
            return false;
        }

        if (Time.time < skill.LastAttackTime + skill.Cooldown)
        {
            return false;
        }

        // Allow secondary skill to attack while moving.
        if (!skill.CanAttackWhileMoving &&
            this.GetComponent<Rigidbody>().velocity.magnitude > .1f &&
            skill != SecondarySkill)
        {
            return false;
        }

        float distanceToTarget = GetDistBetweenColliders(Target.Body.BoxCollider, this.Body.BoxCollider);
        if (distanceToTarget <= GetAttackRange(skill))
        {
            skill.Activate(this, BuildAttackTargetingDetails());
            return true;
        }
        else
        {
            if (distanceToTarget > TargetFindRadius)
            {
                Target = null;
            }
        }

        return false;
    }

    private void ApplyPassiveEffects()
    {
        foreach (PassiveSkill passiveSkill in PassiveSkills)
        {
            if (Time.time < passiveSkill.LastAttackTime + passiveSkill.Cooldown)
            {
                continue;
            }

            passiveSkill.Activate(this, BuildAttackTargetingDetails());
        }
    }

    private AttackTargetingDetails BuildAttackTargetingDetails()
    {
        return new AttackTargetingDetails
        {
            Attacker = this,
            Target = this.Target,
        };
    }

    protected float timeBetweenTargetChecks = .5f;
    protected float lastTargetCheckTime;
    protected void CheckForTarget()
    {
        if (Time.time + lastTargetCheckTime > timeBetweenTargetChecks)
        {
            this.Target = this.FindTarget();
            lastTargetCheckTime = Time.time;
        }
    }

    public float TargetFindRadius;
    protected Character FindTarget()
    {
        Collider[] nearby = Physics.OverlapSphere(this.transform.position, TargetFindRadius);
        Character closest = null;
        float closestDist = float.MaxValue;
        foreach (Collider collider in nearby)
        {
            // Get from parent because collider is always on body.
            Character character = collider.transform.parent?.GetComponent<Character>();
            if (character == null)
            {
                continue;
            }

            if (this.Enemies.Contains(character.Allegiance))
            {
                float distance = Vector3.Distance(collider.transform.position, this.transform.position);
                if (distance < closestDist)
                {
                    closest = character;
                    closestDist = distance;
                }
            }
        }

        return closest;
    }

    public void Heal(int amount)
    {
        this.Health += amount;
        this.Health = Mathf.Min(this.Health, this.MaxHealth);
    }

    protected virtual void SetVelocity() { }
    protected virtual void OnDeath()
    {
        Destroy(this.Healthbar.gameObject);
        Destroy(this.gameObject.gameObject);
    }

    private float lastManaRegenTime;
    protected void RegenerateMana()
    {
        if (Time.time > lastManaRegenTime + (60f / ManaRegenPerMinute))
        {
            Mana += 1;
            lastManaRegenTime = Time.time;
        }
    }

    public void TakeDamage(int amount, Character attacker)
    {
        if (IsDead)
        {
            return;
        }

        this.Health -= amount;
        GameObject inst = Instantiate(Constants.Prefabs.DamageNumber, new Vector3(1000, 1000, 1000),
            new Quaternion(), Constants.Persistant.DamageUIParent);
        inst.GetComponent<OnScreenNumber>().SetValue(amount, this.gameObject, null);

        if (this.Health <= 0)
        {
            this.Health = 0;
            this.IsDead = true;
            OnDeath();
        }

        this.Healthbar.SetFillScale((float)this.Health / this.MaxHealth);
    }

    public virtual void Interact()
    {
        Debug.Log($"Clicked on {this.Name}");
    }

    private Quaternion initialRotation;
    protected void SetRotationWithVelocity()
    {
        if (rb.velocity.magnitude < .1f)
        {
            return;
        }

        Quaternion lookRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
        Body.Transform.rotation = lookRotation * initialRotation;
    }

    protected void LookTowards(Vector3 spot)
    {
        Quaternion lookRotation = Quaternion.LookRotation(spot - this.transform.position, Vector3.up);
        Body.Transform.rotation = lookRotation * initialRotation;
    }

    public void RecalculateItemEffects()
    {
        SetInitialStats();
        WornItems.ApplyItemEffects(this);
    }


    protected float GetAttackRange(Skill skill)
    {
        if (skill is MeleeSkill)
        {
            return MeleeAttackRange;
        }
        else
        {
            return RangedAttackRange;
        }
    }

    protected abstract void SetInitialStats();

    private float GetDistBetweenColliders(Collider c1, Collider c2)
    {
        Vector3 closestC1 = c1.ClosestPoint(c2.transform.position);
        Vector3 closestC2 = c2.ClosestPoint(c1.transform.position);
        return Vector3.Distance(closestC1, closestC2);
    }

    protected void MoveTowards(Vector3 targetPosition)
    {
        Vector3 diff = targetPosition - this.transform.position;
        diff.y = 0;
        this.rb.velocity = diff.normalized * MovementSpeed;
        SetRotationWithVelocity();
    }

    protected Vector3 GetVectorTo(Vector3 targetPos)
    {
        Vector3 diffVector = targetPos - this.transform.position;
        diffVector.y = 0;
        return diffVector;
    }
}