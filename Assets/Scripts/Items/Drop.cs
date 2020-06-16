using UnityEngine;

public class Drop : MonoBehaviour 
{
    public Item Item;

    protected float lastPickupAttemptTime;

    void Start()
    {
        this.Item = new Stick();
        lastPickupAttemptTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < lastPickupAttemptTime + .5f)
        {
            return;
        }

        lastPickupAttemptTime = Time.time;

        if (other.CompareTag(Constants.Tags.Player)){
            Player player = other.GetComponent<Player>();
            if (!player.Inventory.IsFull())
            {
                player.Inventory.AddItem(this.Item);
            }
            
            Destroy(this.gameObject);
        }
    }
}