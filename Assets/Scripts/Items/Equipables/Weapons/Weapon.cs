using UnityEngine;
using System.Collections;

public abstract class Weapon : EquipableItem
{
    public GameObject Instantiation;
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.MainHand;
    public abstract SkillType DefaultAttack { get; }
    public abstract PlayerAnimationState IdleAnimation { get; }
    public abstract PlayerAnimationState AttackAnimation { get; }
    public abstract PlayerAnimationState WalkAnimation { get; }
    public abstract PlayerAnimationState RunAnimation { get; }
    public virtual PlayerAnimationState SpellAnimation => AttackAnimation;
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
        if (HasInstantiation)
        {
            Instantiation = GameObject.Instantiate(
                        Prefab,
                        bearer.Body.MainHand.transform);
            Instantiation.transform.position = bearer.Body.MainHand.transform.position;

            foreach (Collider c in Instantiation.GetComponentsInChildren<Collider>())
            {
                c.enabled = false;
            }

            this.projectileStartPositionObj = Instantiation?.transform.Find("ProjectileStartPosition") ?? Instantiation.transform;
        }
        else
        {
            this.projectileStartPositionObj = bearer.Body.MainHand.transform;
        }


        if (bearer.PrimarySkill == null)
        {
            // TODO: set through selection.
            bearer.SetAbility(0, DefaultAttack);
        }
    }

    public override void OnUnequip(Character bearer)
    {
        base.OnUnequip(bearer);
        GameObject.Destroy(Instantiation);
    }
}
