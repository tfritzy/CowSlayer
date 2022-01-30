using UnityEngine;

public class SwordSwing : MeleeSkill
{
    public override string Name => "Sword Swing";
    public override float Cooldown => 1.5f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.SwordSwing;
    public override float DamageModifier => 1f;

    public SwordSwing(Character bearer) : base(bearer)
    {
    }
}