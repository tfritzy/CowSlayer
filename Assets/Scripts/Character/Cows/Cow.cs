using UnityEngine;

public class Cow : Character
{
    public CowState CurrentState;
    public float MovementSpeed;

    protected Rigidbody rb;
    public enum CowState
    {
        Grazing,
        Attacking
    }

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    void Update()
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

    protected float lastTargetCheckTime;
    protected void CheckForTarget(){
        if (Target == null && Time.time + lastTargetCheckTime > 1f){
            this.Target = this.FindTarget();
            if (this.Target != null){
                this.CurrentState = CowState.Attacking;
            }
        }
    }

    protected void Attack(){
        Vector2 diffVector = Vector2.MoveTowards((Vector2)this.transform.position, Target.transform.position, 1000);
        this.rb.velocity = diffVector.normalized * this.MovementSpeed;
    }

    private Vector2 grazeTargetPosition;
    private float lastGrazePositionTimeChange;
    private const float timeBetweenGrazePositionChanges = 5f;
    protected void Graze()
    {
        if (Time.time > lastGrazePositionTimeChange + timeBetweenGrazePositionChanges)
        {
            this.grazeTargetPosition = FindNewGrazePosition();
            lastGrazePositionTimeChange = Time.time;
        }
        Vector2 diffVector = Vector2.MoveTowards((Vector2)this.transform.position, this.grazeTargetPosition, 1000);
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

    private Vector2 FindNewGrazePosition()
    {
        return new Vector2(Random.Range(-.75f, .75f), Random.Range(-.75f, .75f));
    }
}