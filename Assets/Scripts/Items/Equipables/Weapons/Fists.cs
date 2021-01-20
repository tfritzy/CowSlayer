using System.Collections.Generic;
using UnityEngine;

public class Fists : Weapon
{
    public override SkillType DefaultAttack => SkillType.Punch;
    public override AnimationState IdleAnimation => AnimationState.IdleNoWeapon;
    public override AnimationState AttackAnimation => AnimationState.Punch;
    public override AnimationState WalkAnimation => AnimationState.NormalWalk;
    public override AnimationState SpellAnimation => AnimationState.CastSpellBareHanded;
    public override string Name => "Fists";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(1, 1);
    protected override List<Effect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;

    public override Vector3 ProjectileStartPosition => Constants.Persistant.PlayerScript.Body.MainHand.transform.position;
}