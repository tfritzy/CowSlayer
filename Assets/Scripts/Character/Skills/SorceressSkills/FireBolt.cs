public class FireBolt : RangedSkill
{
    public override string Name => "Fire Bolt";
    public override float Cooldown => 1f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 5;
    public override SkillType Type => SkillType.Firebolt;
    public override float DamageModifier => 2f + 0.20f * Level;
    protected override float ProjectileSpeed => 14f;
    public override float Range => 8f;

    public FireBolt(Character bearer) : base(bearer)
    {
    }
}