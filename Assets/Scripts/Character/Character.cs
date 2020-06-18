using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, Interactable
{
    public int Health;
    protected int MaxHealth;
    public int Damage;
    public float AttackSpeed;
    protected float TimeBetweenAttacks {
        get {
            return 1 / Mathf.Pow(AttackSpeed, .6f);
        }
    }
    protected bool CanAttackWhileMoving => false;
    public float AttackRange;
    public GameObject Target;
    public string Name;
    public Allegiance Allegiance;
    public HashSet<Allegiance> Enemies;
    public Body Body;
    protected Healthbar Healthbar;
    protected GameObject DamageNumberPrefab;

    public virtual void Initialize()
    {
        this.DamageNumberPrefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/DamageNumber");
        this.Healthbar = Instantiate(Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/Healthbar"), Vector3.zero,
            new Quaternion(), Constants.GameObjects.HealthUIParent).GetComponent<Healthbar>();
        this.Healthbar.SetOwner(this.transform);
        this.Body = new Body(this.transform.Find("Body"));
    }

    void Start()
    {
        Initialize();
    }

    protected virtual void UpdateLoop()
    {
        CheckForTarget();
        Attack();
        SetVelocity();
    }

    void Update() 
    {
        UpdateLoop();
    }
    
    protected float lastAttackTime;
    public virtual void Attack(){
        if (Target == null || Time.time - lastAttackTime < TimeBetweenAttacks)
        {
            return;
        }

        if (!CanAttackWhileMoving && this.GetComponent<Rigidbody>().velocity.magnitude > .1f)
        {
            return;
        }

        float distance = Vector3.Distance(Target.transform.position, this.transform.position);
        if (distance <= AttackRange)
        {
            Target.GetComponent<Character>().TakeDamage(this.Damage, this);    
            lastAttackTime = Time.time;
        } else
        {
            if (distance > TargetFindRadius)
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
    protected virtual void OnDeath() { }

    public void TakeDamage(int amount, Character attacker)
    {
        this.Health -= amount;
        GameObject inst = Instantiate(DamageNumberPrefab, new Vector3(1000, 1000, 1000),
            new Quaternion(), Constants.GameObjects.DamageUIParent);
        inst.GetComponent<DamageNumber>().SetValue(amount, this.gameObject);
        if (this.Health <= 0)
        {
            this.Health = 0;
            OnDeath();
            Destroy(this.Healthbar.gameObject);
            Destroy(this.gameObject.gameObject);
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
}