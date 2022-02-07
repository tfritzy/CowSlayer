using UnityEngine;
using System.Collections;
using System;

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

    public virtual Vector3 ProjectileStartPosition
    {
        get
        {
            return projectileStartPositionObj.position;
        }
    }
    private Transform projectileStartPositionObj;

    public Weapon(int level) : base(level)
    {
    }

    private static Func<string, float, StatModifier>[] secondaryAttributePool = new Func<string, float, StatModifier>[] {
        (string id, float power) => new FlatDamageStatModifier(id, power),
        (string id, float power) => new MaxHealthStatModifier(id, power),
        (string id, float power) => new MovementSpeedStatModifier(id, power),
        (string id, float power) => new ArmorStatModifier(id, power),
    };
    protected override Func<string, float, StatModifier>[] SecondaryAttributePool => secondaryAttributePool;

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

    public override void OnUnEquip(Character bearer)
    {
        base.OnUnEquip(bearer);
        GameObject.Destroy(Instantiation);
    }
}
