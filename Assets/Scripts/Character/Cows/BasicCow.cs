public class BasicCow : Cow
{

    private CowType cowType = CowType.BasicCow;
    public override CowType CowType => cowType;

    private float manaRegenPerMinute = 50f;
    public override float ManaRegenPerMinute => manaRegenPerMinute;

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
        this.MovementSpeed = 2f;
        this.XPReward = 1 + Level;
        this.PrimarySkill = new Whack(this);
        base.SetInitialStats();
    }
}
