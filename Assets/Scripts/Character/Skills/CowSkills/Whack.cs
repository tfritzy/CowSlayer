public class Whack : MeleeSkill
{
    public override string Name => "Whack";
    public override float Cooldown => 0f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.Whack;
    public override float DamageModifier => 1f;

    public Whack(Character owner) : base(owner) { }

}