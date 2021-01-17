using UnityEngine;

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
        this.MaxHealth = 10 + Level * 2;
        this.Damage = 3 + Level / 2;
        this.AttackSpeedPercent = 1;
        this.TargetFindRadius = 10f;
        this.MeleeAttackRange = 1;
        this.MovementSpeed = 5f;
        this.XPReward = 4 + Level;
        this.PrimarySkill = new Whack();
        base.SetInitialStats();
    }
}
