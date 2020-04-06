using UnityEngine;

public class Character : MonoBehaviour
{
    public int Health;
    public int Damage;
    public float AttackSpeed;

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