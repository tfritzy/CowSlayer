using UnityEngine;
using System.Collections;

public abstract class Weapon : EquipableItem
{
    protected GameObject BodyPrefab;
    private GameObject bodyInst;

    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.MainHand;

    public Weapon()
    {
        BodyPrefab = Resources.Load<GameObject>($"{Constants.FilePaths.WeaponPrefabs}/{Name}");
    }

    public override void OnEquip()
    {
        base.OnEquip();
        bodyInst = GameObject.Instantiate(
            BodyPrefab,
            Constants.GameObjects.PlayerScript.Body.OffHand.transform.position,
            new Quaternion(),
            Constants.GameObjects.PlayerScript.Body.OffHand.transform);
    }

    public override void OnUnequip()
    {
        base.OnUnequip();
        GameObject.Destroy(bodyInst);
    }

}
