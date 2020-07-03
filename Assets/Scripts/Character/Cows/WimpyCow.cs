using UnityEngine;
using System.Collections;
using System;

public class WimpyCow : Cow
{
    public override CowType CowType => CowType.WimpyCow;

    public override void Initialize()
    {
        this.Name = "Wimpy Cow " + Guid.NewGuid().ToString("N").Substring(0, 8);
        this.DropTable = new WimpyCowDropTable();
        base.Initialize();
    }

    protected override void SetInitialStats()
    {
        this.Health = 5;
        this.MaxHealth = Health;
        this.Damage = 2;
        this.AttackSpeed = 1;
        this.TargetFindRadius = 3;
        this.AttackRange = 2f;
        this.MovementSpeed = 2f;
        this.PrimarySkill = new Whack();
    }
}
