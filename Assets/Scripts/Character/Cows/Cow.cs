using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public abstract class Cow : Character
{
    public CowState CurrentState;
    public float MovementSpeed;
    public DropTable DropTable;

    protected Rigidbody rb;
    public enum CowState
    {
        Grazing,
        Attacking
    }

    protected override void UpdateLoop()
    {
        AIUpdate();
    }

    protected virtual void AIUpdate()
    {
        switch (this.CurrentState)
        {
            case CowState.Grazing:
                Graze();
                break;
            case CowState.Attacking:
                Attack();
                break;
            default:
                Graze();
                break;
        }
    }

    public override void Initialize() {
        base.Initialize();
        this.Allegiance = Allegiance.Cows;
        this.Enemies = new HashSet<Allegiance>() {Allegiance.Player};
        this.rb = this.GetComponent<Rigidbody>();
        this.name = this.Name;
    }

    private Vector3 grazeTargetPosition;
    private float lastGrazePositionTimeChange;
    private const float timeBetweenGrazePositionChanges = 5f;
    protected void Graze()
    {
        if (Time.time > lastGrazePositionTimeChange + timeBetweenGrazePositionChanges)
        {
            this.grazeTargetPosition = FindNewGrazePosition();
            lastGrazePositionTimeChange = Time.time;
        }
        Vector3 diffVector = Vector3.MoveTowards((Vector2)this.transform.position, this.grazeTargetPosition, 1000);
        float magnitude = diffVector.magnitude;
        if (magnitude < .1f)
        {
            this.rb.velocity = Vector3.zero;
        }
        else
        {
            this.rb.velocity = diffVector.normalized * this.MovementSpeed;
        }
    }

    private Vector3 FindNewGrazePosition()
    {
        return new Vector3(Random.Range(-.75f, .75f), Constants.MapParameters.BlockYPos, Random.Range(-.75f, .75f));
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