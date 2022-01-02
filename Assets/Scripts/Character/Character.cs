using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, Interactable
{
    protected Rigidbody rb;
    public int MaxHealth { get; protected set; }
    public int Damage;
    public float AttackSpeedPercent;
    public WornItemsGroup WornItems;
    protected float TimeBetweenAttacks
    {
        // base is 1s, so if attackSpeed = 100%, timeBetweenAttacks == 1. if it's 200% it's .5s. 400% == .25s
        get { return 1 / AttackSpeedPercent; }
    }
    protected bool CanAttackWhileMoving => false;
    public float MeleeAttackRange;
    public float RangedAttackRange;
    private Skill _primarySkill;
    public Skill PrimarySkill
    {
        get
        {
            if (_primarySkill == null)
            {
                return Constants.GetSkill(WornItems.Weapon.DefaultAttack);
            }

            return _primarySkill;
        }
        set { _primarySkill = value; }
    }
    private Skill _secondarySkill;
    public Skill SecondarySkill
    {
        get
        {
            if (_secondarySkill == null)
            {
                return Constants.GetSkill(WornItems.Weapon.DefaultAttack);
            }

            return _secondarySkill;
        }
        set { _secondarySkill = value; }
    }
    public string Name;
    public Allegiance Allegiance;
    public HashSet<Allegiance> Enemies;
    public Body Body;
    public int MaxMana;
    public virtual int Level { get; set; }
    public abstract float ManaRegenPerMinute { get; }
    protected Healthbar Healthbar;
    protected bool IsDead;
    public float MovementSpeed;
    public virtual float TurnRateDegPerS => 60;

    private Vector3 _position;
    public Vector3 Position
    {
        get
        {
            _position = this.transform.position;
            _position.y = 0;
            return _position;
        }
    }

    private Vector3 _forward;
    public Vector3 Forward
    {
        get
        {
            _forward = this.transform.forward;
            _forward.y = 0;
            return _forward;
        }
    }

    private Character _target;
    public virtual Character Target
    {
        get { return _target; }
        set { _target = value; }
    }
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
    protected int _health;
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
        SetRigidbodyConstraints();
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

    protected Skill InProgressAttack;
    protected bool CanPerformAttack(Skill skill)
    {
        if (IsDead || Target == null)
        {
            return false;
        }

        if (InProgressAttack != null)
        {
            return false;
        }

        if (!skill.CanAttackWhileMoving && this.GetComponent<Rigidbody>().velocity.magnitude > .2f)
        {
            return false;
        }

        if (skill.IsOnCooldown())
        {
            return false;
        }

        float distanceToTarget = Helpers.GetDistBetweenColliders(Target.Body.Collider, this.Body.Collider);
        if (distanceToTarget <= GetAttackRange(skill))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Tries to attack the target with the given skill. Returns true if successful, false otherwise.
    /// </summary>
    protected bool PerformAttack(Skill skill)
    {
        if (CanPerformAttack(skill))
        {
            SetAttackAnimation(skill);
            LookTowards(Target.transform.position);
            this.InProgressAttack = skill;
            return true;
        }
        else
        {
            float distanceToTarget = Helpers.GetDistBetweenColliders(Target.Body.Collider, this.Body.Collider);
            if (distanceToTarget > TargetFindRadius)
            {
                Target = null;
            }
            return false;
        }
    }

    protected abstract void SetAttackAnimation(Skill skill);

    public virtual void AttackAnimTrigger()
    {
        this.InProgressAttack.Activate(this, BuildAttackTargetingDetails());
        this.InProgressAttack = null;
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

    protected AttackTargetingDetails BuildAttackTargetingDetails()
    {
        return new AttackTargetingDetails
        {
            Attacker = this,
            Target = this.Target,
        };
    }

    protected float timeBetweenTargetChecks = .5f;
    protected float lastTargetCheckTime;
    protected virtual void CheckForTarget()
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
        GameObject.Destroy(this.Healthbar.gameObject);
        GameObject.Destroy(this.gameObject.gameObject);
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
        spot.y = this.transform.position.y;
        Quaternion lookRotation = Quaternion.LookRotation(spot - this.transform.position, Vector3.up);
        this.Body.Transform.rotation = Quaternion.RotateTowards(initialRotation, lookRotation, 360);
    }

    public void RecalculateItemEffects()
    {
        SetInitialStats();
        WornItems.ApplyItemEffects(this);
    }

    protected float GetSqrAttackRange(Skill skill)
    {
        if (skill is MeleeSkill)
        {
            return MeleeAttackRange * MeleeAttackRange;
        }
        else
        {
            return RangedAttackRange * RangedAttackRange;
        }
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

    protected void MoveTowards(Vector3 targetPosition)
    {
        Vector3 diff = targetPosition - this.transform.position;
        diff.y = 0;
        this.rb.velocity = diff.normalized * MovementSpeed;
        SetRotationWithVelocity();
    }

    protected void SetRigidbodyConstraints()
    {
        this.rb.constraints =
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotation;
    }

    protected void FreezeRigidbody()
    {
        this.rb.constraints =
            RigidbodyConstraints.FreezePosition |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
    }

    public virtual void SetAbility(int index, SkillType? skill)
    {
        if (skill == null)
        {
            return;
        }

        if (index == 0)
        {
            PrimarySkill = Constants.GetSkill(skill.Value);
        }
        else
        {
            SecondarySkill = Constants.GetSkill(skill.Value);
        }
    }

    protected void SetVelocityTowardsPoint(Vector3 point, float velocity)
    {
        this.rb.velocity = Vector3.Lerp(
            this.Forward * velocity,
            (point - this.Position).normalized * velocity,
            Time.deltaTime * this.TurnRateDegPerS);
    }

    protected void RotateTowardsPoint(Vector3 point)
    {
        point.y = 0;
        this.Body.Transform.rotation = Quaternion.Slerp(
            this.Body.Rotation,
            Quaternion.LookRotation(point - this.Position),
            Time.deltaTime * this.TurnRateDegPerS);
    }
}