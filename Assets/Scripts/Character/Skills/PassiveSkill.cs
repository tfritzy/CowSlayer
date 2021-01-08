public abstract class PassiveSkill : Skill
{
    public PassiveSkill(Character owner) : base(owner)
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