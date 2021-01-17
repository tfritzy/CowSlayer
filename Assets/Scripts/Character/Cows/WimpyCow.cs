public class WimpyCow : Cow
{
    public override CowType CowType => CowType.WimpyCow;
    public override float ManaRegenPerMinute => 50f;

    public override void Initialize()
    {
        this.Name = "Wimpy Cow";
        this.DropTable = new WimpyCowDropTable();
        base.Initialize();
    }

    protected override void SetInitialStats()
    {
        this.MaxHealth = 5 + Level;
        this.Damage = 2 + Level / 2;
        this.AttackSpeedPercent = 1;
        this.TargetFindRadius = 5f;
        this.MeleeAttackRange = .5f;
        this.MovementSpeed = 2f;
        this.XPReward = 1 + Level;
        this.PrimarySkill = new Whack();
        base.SetInitialStats();
    }
}
