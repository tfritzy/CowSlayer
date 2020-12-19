public class FireBolt : RangedSkill
{
    public override string Name => "Fire Bolt";
    public override float Cooldown => 1f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 5;

    protected override float MovementSpeed => 14f;
}