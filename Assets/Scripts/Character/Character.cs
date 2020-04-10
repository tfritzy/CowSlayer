using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int Health;
    public int Damage;
    public float AttackSpeed;
    public GameObject Target;

    public Allegiance Allegiance;
    public HashSet<Allegiance> Enemies;
    
    protected abstract void Initialize();

    protected abstract float TargetFindRadius { get; }
    protected GameObject FindTarget() {
        Collider[] nearby = Physics.OverlapSphere(this.transform.position, TargetFindRadius, Constants.Layers.Character);
        GameObject closest = null;
        float closestDist = float.MaxValue;
        foreach (Collider collider in nearby){
            Character character = collider.GetComponent<Character>();
            if (this.Enemies.Contains(character.Allegiance)){
                float distance = Vector3.Distance(collider.transform.position, this.transform.position);
                if (distance < closestDist){
                    closest = collider.gameObject;
                    closestDist = distance;
                }
            }
        }
        return closest;
    }

    public void TakeDamage(int amount, Character attacker)
    {
        this.Health -= amount;
        if (this.Health <= 0)
        {
            this.Health = 0;
            Destroy(this.gameObject);
        }
    }
}