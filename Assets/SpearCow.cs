using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCow : Cow
{
    public override CowType CowType => CowType.SpearThrower;

    public override void Initialize()
    {
        this.Name = "Spear Cow";
        this.DropTable = new BasicCowDropTable();
        base.Initialize();
    }

    protected override void SetInitialStats()
    {
        this.MaxHealth = 10 + Level * 2;
        this.Damage = 3 + Level;
        this.AttackSpeedPercent = 1;
        this.TargetFindRadius = 7f;
        this.MeleeAttackRange = .1f;
        this.RangedAttackRange = 5f;
        this.MovementSpeed = 2f;
        this.XPReward = 1 + Level;
        this.PrimarySkill = new SpearThrow(this);
        base.SetInitialStats();
    }
}
