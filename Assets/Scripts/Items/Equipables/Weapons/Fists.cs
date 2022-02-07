using System;
using System.Collections.Generic;
using UnityEngine;

public class Fists : Weapon
{
    public Fists(int level) : base(level)
    {
    }

    public override SkillType DefaultAttack => SkillType.Punch;
    public override PlayerAnimationState IdleAnimation => PlayerAnimationState.IdleNoWeapon;
    public override PlayerAnimationState AttackAnimation => PlayerAnimationState.Punch;
    public override PlayerAnimationState WalkAnimation => PlayerAnimationState.WalkNoWeapon;
    public override PlayerAnimationState SpellAnimation => PlayerAnimationState.CastSpellBareHanded;
    public override PlayerAnimationState RunAnimation => PlayerAnimationState.RunNoWeapon;
    public override string Name => "Fists";
    public override ItemRarity Rarity => ItemRarity.Common;
    public override bool HasInstantiation => false;
    private static Func<string, float, StatModifier>[] secondaryAttributePool = new Func<string, float, StatModifier>[] { };
    protected override Func<string, float, StatModifier>[] SecondaryAttributePool => secondaryAttributePool;

    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            new FlatDamageStatModifier(this.Id, this.BasePower * 1f),
        };
    }
}