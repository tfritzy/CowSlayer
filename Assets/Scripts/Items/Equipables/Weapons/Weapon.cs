using UnityEngine;
using System.Collections;

public abstract class Weapon : EquipableItem
{
    private GameObject bodyInst;

    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.MainHand;

    public abstract Skill Effect { get; }

    public Character Bearer;

    public Weapon()
    {
    }

    public override void OnEquip(Character bearer)
    {
        base.OnEquip(bearer);
        bodyInst = GameObject.Instantiate(
            Prefab,
            bearer.Body.OffHand.transform);
        bodyInst.transform.position = bearer.Body.OffHand.transform.position;
        bodyInst.GetComponent<Collider>().enabled = false;

        // TODO: set through selection.
        bearer.PrimarySkill = Effect;
    }

    public override void OnUnequip(Character bearer)
    {
        base.OnUnequip(bearer);
        GameObject.Destroy(bodyInst);
    }
}
