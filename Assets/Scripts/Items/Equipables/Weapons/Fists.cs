using System.Collections.Generic;
using UnityEngine;

public class Fists : Weapon
{
    public override SkillType DefaultAttack => SkillType.Punch;
    public override PlayerAnimationState IdleAnimation => PlayerAnimationState.IdleNoWeapon;
    public override PlayerAnimationState AttackAnimation => PlayerAnimationState.Punch;
    public override PlayerAnimationState WalkAnimation => PlayerAnimationState.WalkNoWeapon;
    public override PlayerAnimationState SpellAnimation => PlayerAnimationState.CastSpellBareHanded;
    public override PlayerAnimationState RunAnimation => PlayerAnimationState.RunNoWeapon;
    public override string Name => "Fists";
    public override ItemRarity Rarity => ItemRarity.Common;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(1, 1, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => null;
    protected override int NumSecondaryEffects => 0;
    public override bool HasInstantiation => false;
}