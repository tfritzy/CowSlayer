using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCow : Cow
{

    private CowType cowType = CowType.SpearThrower;
    public override CowType CowType => cowType;

    public override void Initialize()
    {
        this.Name = "Spear Cow";
        this.DropTable = new BasicCowDropTable();
        base.Initialize();
        this.PrimarySkill = new SpearThrow(this);
    }

    protected override void SetInitialStats()
    {
        this.MaxHealth = 10 + Level * 2;
        this.Damage = 3 + Level;
        this.AttackSpeedPercent = 1;
        this.TargetFindRadius = 7f;
        this.MovementSpeed = 2f;
        this.XPReward = 1 + Level;
        base.SetInitialStats();
    }
}
