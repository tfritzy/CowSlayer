using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public abstract class Cow : Character
{
    public CowState CurrentState;
    public float MovementSpeed;
    public DropTable DropTable;
    public abstract CowType CowType { get; }

    protected Rigidbody rb;
    public enum CowState
    {
        Grazing,
        Attacking
    }

    protected override void UpdateLoop()
    {
        AIUpdate();
        base.UpdateLoop();
    }

    protected virtual void AIUpdate()
    {
        switch (this.CurrentState)
        {
            case CowState.Grazing:
                Graze();
                break;
            case CowState.Attacking:
                PrimaryAttack();
                break;
            default:
                Graze();
                break;
        }
    }

    protected override void SetVelocity()
    {
        Vector3 diffVector = this.targetPosition - this.transform.position;
        diffVector.y = 0;
        if (diffVector.magnitude < .1f)
        {
            this.rb.velocity = Vector3.zero;
            return;
        }

        this.rb.velocity = diffVector.normalized * MovementSpeed;
        SetRotationWithVelocity();
    }

    public override void PrimaryAttack()
    {
        if (Target == null)
        {
            if (this.CurrentState == CowState.Attacking)
            {
                this.CurrentState = CowState.Grazing;
                this.targetPosition = FindNewGrazePosition();
            }
            
            return;
        }

        if ((Target.transform.position - this.transform.position).magnitude > AttackRange * .8f)
        {
            this.targetPosition = Target.transform.position;
        } else
        {
            targetPosition = this.transform.position;
        }
        
        base.PrimaryAttack();
    }

    public override void Initialize() {
        this.Allegiance = Allegiance.Cows;
        this.Enemies = new HashSet<Allegiance>() { Allegiance.Player };
        this.rb = this.GetComponent<Rigidbody>();
        this.name = this.Name;
        this.targetPosition = FindNewGrazePosition();
        base.Initialize();
    }

    private Vector3 targetPosition;
    private float lastGrazePositionTimeChange;
    private const float timeBetweenGrazePositionChanges = 5f;
    protected void Graze()
    {
        if (Time.time > lastGrazePositionTimeChange + timeBetweenGrazePositionChanges)
        {
            this.targetPosition = FindNewGrazePosition();
            lastGrazePositionTimeChange = Time.time;
        }

        CheckForTarget();
        if (Target != null)
        {
            this.CurrentState = CowState.Attacking;
        }
    }

    private Vector3 FindNewGrazePosition()
    {
        return this.transform.position + new Vector3(Random.Range(-5f, 5f), this.transform.position.y, Random.Range(-5f, 5f));
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Drop drop = DropTable.RollDrop();
        if (drop == null)
        {
            return;
        }

        GameObject dropContainer = GameObject.Instantiate<GameObject>(Constants.Prefabs.EmptyDrop, this.transform.position, new Quaternion(), null);
        dropContainer.GetComponent<DropContainer>().SetDrop(drop);
    }
}