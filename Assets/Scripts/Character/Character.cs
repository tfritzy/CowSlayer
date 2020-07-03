using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, Interactable
{
    public int Health;
    protected int MaxHealth;
    public int Damage;
    public float AttackSpeed;
    public WornItemsGroup WornItems;
    protected float TimeBetweenAttacks {
        get {
            return 1 / Mathf.Pow(AttackSpeed, .6f);
        }
    }
    protected bool CanAttackWhileMoving => false;
    public float AttackRange;
    public Skill PrimarySkill;
    public GameObject Target;
    public string Name;
    public Allegiance Allegiance;
    public HashSet<Allegiance> Enemies;
    public Body Body;
    protected Healthbar Healthbar;
    protected GameObject DamageNumberPrefab;
    protected bool IsDead;

    public virtual void Initialize()
    {
        this.DamageNumberPrefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/DamageNumber");
        this.Healthbar = Instantiate(Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/Healthbar"), Vector3.zero,
            new Quaternion(), Constants.GameObjects.HealthUIParent).GetComponent<Healthbar>();
        this.Healthbar.SetOwner(this.transform);
        this.Body = new Body(this.transform.Find("Body"));
        this.IsDead = false;
        this.WornItems = new WornItemsGroup("Equipped Items", this);
        this.SetInitialStats();
        this.MaxHealth = Health;
    }

    void Start()
    {
        Initialize();
    }

    protected virtual void UpdateLoop()
    {
        CheckForTarget();
        PrimaryAttack();
        SetVelocity();
    }

    void Update() 
    {
        UpdateLoop();
    }
    
    public virtual void PrimaryAttack()
    {
        if (IsDead)
        {
            return;
        }

        if (Target == null || Time.time - PrimarySkill.LastAttackTime < PrimarySkill.Cooldown)
        {
            return;
        }

        if (!PrimarySkill.CanAttackWhileMoving && this.GetComponent<Rigidbody>().velocity.magnitude > .1f)
        {
            return;
        }

        float distanceToTarget = Vector3.Distance(Target.transform.position, this.transform.position);
        if (distanceToTarget <= AttackRange)
        {
            AttackTargetingDetails targetingDetails = new AttackTargetingDetails {
                Target = Target.GetComponent<Character>(),
                TargetPosition = Target.transform.position,
                TravelDirection = Target.transform.position - this.transform.position
            };

            PrimarySkill.Attack(this, targetingDetails);
        } 
        else
        {
            if (distanceToTarget > TargetFindRadius)
            {
                Target = null;
            }
        }
    }

    protected float timeBetweenTargetChecks = .5f;
    protected float lastTargetCheckTime;
    protected void CheckForTarget(){
        if (Time.time + lastTargetCheckTime > timeBetweenTargetChecks){
            this.Target = this.FindTarget();
            lastTargetCheckTime = Time.time;
        }
    }

    public float TargetFindRadius;
    protected GameObject FindTarget() {
        Collider[] nearby = Physics.OverlapSphere(this.transform.position, TargetFindRadius);
        GameObject closest = null;
        float closestDist = float.MaxValue;
        foreach (Collider collider in nearby){
            Character character = collider.GetComponent<Character>();
            if (character == null){
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

    protected abstract void SetVelocity();
    protected virtual void OnDeath() 
    {
        Destroy(this.Healthbar.gameObject);
        Destroy(this.gameObject.gameObject);
    }

    public void TakeDamage(int amount, Character attacker)
    {
        if (IsDead)
        {
            return;
        }

        this.Health -= amount;
        GameObject inst = Instantiate(DamageNumberPrefab, new Vector3(1000, 1000, 1000),
            new Quaternion(), Constants.GameObjects.DamageUIParent);
        inst.GetComponent<DamageNumber>().SetValue(amount, this.gameObject);

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

    protected void SetRotationWithVelocity()
    {
        Vector3 relativePos = this.GetComponent<Rigidbody>().velocity;
        if (relativePos.magnitude < .1f)
        {
            return;
        }
        Quaternion lookRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        Body.Transform.rotation = lookRotation;
    }

    public void RecalculateItemEffects()
    {
        SetInitialStats();
        WornItems.ApplyItemEffects(this);
    }

    protected abstract void SetInitialStats();
}