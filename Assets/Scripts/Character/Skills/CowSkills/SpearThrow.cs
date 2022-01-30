using UnityEngine;

public class SpearThrow : RangedSkill
{
    public override string Name => "Spear Throw";
    public override float Cooldown => 3f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.SpearThrow;
    public override float DamageModifier => 1f;
    protected override float ProjectileSpeed => 10f;
    public override float Range => 8f;

    public SpearThrow(Character bearer) : base(bearer)
    {
    }
}