public class BasicCow : Cow
{
    public override CowType CowType => CowType.BasicCow;
    public override float ManaRegenPerMinute => 50f;

    public override void Initialize()
    {
        this.Name = "Wimpy Cow";
        this.DropTable = new BasicCowDropTable();
        base.Initialize();
    }

    protected override void SetInitialStats()
    {
        this.MaxHealth = 5 + Level * 2;
        this.Damage = 2 + Level;
        this.AttackSpeedPercent = 1;
        this.TargetFindRadius = 5f;
        this.MeleeAttackRange = .1f;
        this.MovementSpeed = 2f;
        this.XPReward = 1 + Level;
        this.PrimarySkill = new Whack(this);
        base.SetInitialStats();
    }
}
