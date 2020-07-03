public class Whack : Skill
{
    public override string Name => "Whack";
    public override float Cooldown => 2f;
    public override bool CanAttackWhileMoving => false;
}