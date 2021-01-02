public abstract class PassiveSkill : Skill
{
    public PassiveSkill()
    {
        Constants.Persistant.PlayerScript.PassiveSkills.Add(this);
    }

    public override bool Activate(Character attacker, AttackTargetingDetails targetingDetails)
    {
        if (base.Activate(attacker, targetingDetails) == false)
        {
            return false;
        }

        ApplyEffect();
        return true;
    }

    public abstract void ApplyEffect();
}