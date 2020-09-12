public class FireBolt : RangedSkill
{
    public override string Name => "Fire Bolt";
    public override float Cooldown => 1f;
    public override bool CanAttackWhileMoving => false;
    public override float DamagePercentIncrease => 1.5f;
    protected override float MovementSpeed => 14f;
    protected override string AttackPrefabName => "Fire Bolt";
}