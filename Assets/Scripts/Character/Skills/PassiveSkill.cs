public abstract class PassiveSkill : Skill
{

    private float range = 0f;
    public override float Range => range;

    public PassiveSkill(Character bearer) : base(bearer)
    {
        Constants.Persistant.PlayerScript.PassiveSkills.Add(this);
    }

    public override bool Activate(Character attacker, AttackTargetingDetails targetingDetails)
    {
        if (base.Activate(attacker, targetingDetails) == false)
        {
            return false;
        }

        ApplyEffect(targetingDetails);
        return true;
    }

    public abstract void ApplyEffect(AttackTargetingDetails targetingDetails);


}