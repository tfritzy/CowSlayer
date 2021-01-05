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

    public int MaxHealth { get; protected set; }
    public int Damage;
    public float AttackSpeed;
    public WornItemsGroup WornItems;
    protected float TimeBetweenAttacks
    {
        get
        {
            return 1 / Mathf.Pow(AttackSpeed, .6f);
        }
    }
    protected bool CanAttackWhileMoving => false;
    public float MeleeAttackRange;
    public float RangedAttackRange;
    public Skill PrimarySkill;
    public Skill SecondarySkill;
    public GameObject Target;
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
    }

    void Awake()
    {
        Initialize();
    }

    protected virtual void UpdateLoop()
    {
        CheckForTarget();
        PrimaryAttack();
        SetVelocity();
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

        float distanceToTarget = GetDistBetweenColliders(Target.GetComponent<Collider>(), this.GetComponent<Collider>());
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
            Target = Target?.GetComponent<Character>(),
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
    protected GameObject FindTarget()
    {
        Collider[] nearby = Physics.OverlapSphere(this.transform.position, TargetFindRadius);
        GameObject closest = null;
        float closestDist = float.MaxValue;
        foreach (Collider collider in nearby)
        {
            Character character = collider.GetComponent<Character>();
            if (character == null)
            {
                continue;
            }
            if (this.Enemies.Contains(character.Allegiance))
            {
                float distance = Vector3.Distance(collider.transform.position, this.transform.position);
                if (distance < closestDist)
                {
                    closest = collider.gameObject;
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

    protected abstract void SetVelocity();
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
        Vector3 relativePos = this.GetComponent<Rigidbody>().velocity;
        if (relativePos.magnitude < .1f)
        {
            return;
        }
        Quaternion lookRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        Body.Transform.rotation = lookRotation * initialRotation;
    }

    public void RecalculateItemEffects()
    {
        SetInitialStats();
        WornItems.ApplyItemEffects(this);
    }


    private float GetAttackRange(Skill skill)
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
}