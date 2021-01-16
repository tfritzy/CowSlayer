public class ChargeCow : Cow
{
    public override CowType CowType => CowType.ChargeCow;
    public override float ManaRegenPerMinute => 50f;

    public override void Initialize()
    {
        this.Name = "Charge Cow";
        this.DropTable = new WimpyCowDropTable();
        base.Initialize();
    }

    protected override void SetInitialStats()
    {
        this.MaxHealth = 5 + Level;
        this.Damage = 2 + Level / 2;
        this.AttackSpeedPercent = 1;
        this.TargetFindRadius = 8f;
        this.MeleeAttackRange = 5;
        this.MovementSpeed = 2f;
        this.XPReward = 1 + Level;
        this.PrimarySkill = new Charge(this);
        base.SetInitialStats();
    }

    public override void AttackLoop()
    {

    }
}
