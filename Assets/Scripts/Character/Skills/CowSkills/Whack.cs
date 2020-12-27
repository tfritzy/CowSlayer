public class Whack : MeleeSkill
{
    public override string Name => "Whack";
    public override float Cooldown => 2f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.Whack;

    protected override string IconFilePath => $"{Constants.FilePaths.Icons}/DaggerIcon";
    
}