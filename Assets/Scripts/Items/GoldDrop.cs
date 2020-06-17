using UnityEngine;

public class GoldDrop : Drop 
{
    public int Value;
    
    public GoldDrop(int low, int high)
    {
        Value = Random.Range(low, high);
    }

    public override bool GiveDropToPlayer(Player player)
    {
        player.Gold += Value;
        return true;
    }

    public override void SetModel(Transform container)
    {
        GameObject.Instantiate(Constants.Prefabs.Gold.SmallGoldPile, container.position, new Quaternion(), container);
    }
}