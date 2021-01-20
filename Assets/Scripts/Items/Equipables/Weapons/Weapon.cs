using UnityEngine;
using System.Collections;

public abstract class Weapon : EquipableItem
{
    public GameObject Instantiation;
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.MainHand;
    public abstract SkillType DefaultAttack { get; }
    public abstract AnimationState IdleAnimation { get; }
    public abstract AnimationState AttackAnimation { get; }
    public abstract AnimationState WalkAnimation { get; }
    public virtual AnimationState SpellAnimation => AttackAnimation;
    public Character Bearer;
    public virtual Vector3 ProjectileStartPosition
    {
        get
        {
            return projectileStartPositionObj.position;
        }
    }
    private Transform projectileStartPositionObj;

    public Weapon()
    {
    }

    public override void OnEquip(Character bearer)
    {
        base.OnEquip(bearer);
        Instantiation = GameObject.Instantiate(
            Prefab,
            bearer.Body.MainHand.transform);
        Instantiation.transform.position = bearer.Body.MainHand.transform.position;

        foreach (Collider c in Instantiation.GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }

        this.projectileStartPositionObj = Instantiation.transform.Find("ProjectileStartPosition");

        // TODO: set through selection.
        bearer.SetAbility(0, DefaultAttack);
    }

    public override void OnUnequip(Character bearer)
    {
        base.OnUnequip(bearer);
        GameObject.Destroy(Instantiation);
    }
}
