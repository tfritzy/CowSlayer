using System.Collections.Generic;

public class Crossbow : Weapon
{
    public override string Name => "Crossbow";
    public override ItemRarity Rarity => ItemRarity.Common;
    public override Skill Effect => new CrossbowAttack();
    public override AnimationState IdleAnimationState => AnimationState.IdleOneHandedWeapon;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(2, 4);
    protected override List<Effect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;

}