using UnityEngine;
using System.Collections;
using System;

public class WimpyCow : Cow
{
    public override CowType CowType => CowType.WimpyCow;
    public override float ManaRegenPerMinute => 50f;

    public override void Initialize()
    {

        this.Name = "Wimpy Cow";
        this.DropTable = new WimpyCowDropTable();
        base.Initialize();
    }

    protected override void SetInitialStats()
    {
        this.Health = 5;
        this.MaxHealth = Health;
        this.Damage = 2;
        this.AttackSpeed = 1;
        this.TargetFindRadius = 5f;
        this.MeleeAttackRange = 2f;
        this.MovementSpeed = 2f;
        this.XPReward = 1;
        this.PrimarySkill = new Whack();
        base.SetInitialStats();
    }
}
