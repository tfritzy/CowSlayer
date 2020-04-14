using UnityEngine;

public class Drop : MonoBehaviour 
{
    public Item Item;

    void Start()
    {
        this.Item = new TestItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player)){
            other.GetComponent<Player>().Inventory.Items.Add(this.Item);
            Destroy(this.gameObject);
        }
    }
}