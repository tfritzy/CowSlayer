using System.Collections.Generic;

public class Fists : Weapon
{
    public override SkillType DefaultAttack => SkillType.Punch;
    public override AnimationState IdleAnimation => AnimationState.IdleNoWeapon;
    public override AnimationState AttackAnimation => AnimationState.Punch;
    public override string Name => "Fists";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(1, 1);
    protected override List<Effect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;
}