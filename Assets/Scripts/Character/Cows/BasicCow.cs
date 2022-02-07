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
        this.PrimarySkill = new Whack(this);
        base.Initialize();
    }
}
