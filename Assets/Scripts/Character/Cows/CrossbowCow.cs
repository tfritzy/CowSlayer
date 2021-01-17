using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowCow : Cow
{
    public override CowType CowType => CowType.Crossbow;

    public override float ManaRegenPerMinute => 100;

    public override void Initialize()
    {
        this.Name = "Crossbow Cow";
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
