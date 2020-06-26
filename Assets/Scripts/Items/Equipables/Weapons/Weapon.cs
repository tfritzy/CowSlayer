using UnityEngine;
using System.Collections;

public abstract class Weapon : EquipableItem
{
    private GameObject bodyInst;

    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.MainHand;

    public Weapon()
    {
    }

    public override void OnEquip(Character bearer)
    {
        base.OnEquip(bearer);
        bodyInst = GameObject.Instantiate(
            Prefab,
            bearer.Body.OffHand.transform.position,
            new Quaternion(),
            bearer.Body.OffHand.transform);
    }

    public override void OnUnequip(Character bearer)
    {
        base.OnUnequip(bearer);
        GameObject.Destroy(bodyInst);
    }
}
