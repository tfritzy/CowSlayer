public class Attunement : PassiveSkill
{
    public override string Name => "Attunement";
    public override float Cooldown => 1;
    public override bool CanAttackWhileMoving => true;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.Attunement;
    public override bool IsPassive => true;
    public override float DamageModifier => 0f;
    protected override string IconFilePath => $"{Constants.FilePaths.Icons}/Attunement";

    public override void ApplyEffect()
    {
        Constants.Persistant.PlayerScript.Mana += 1 * Level;
    }
}