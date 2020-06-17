using UnityEngine;
using System.Collections;
using System;

public class WimpyCow : Cow
{
    public override void Initialize()
    {
        this.Health = 10;
        this.MaxHealth = Health;
        this.Damage = 2;
        this.AttackSpeed = 1;
        this.TargetFindRadius = 3;
        this.AttackRange = .5f;
        this.Name = "Wimpy Cow " + Guid.NewGuid().ToString("N").Substring(0, 8);
        this.DropTable = new WimpyCowDropTable();
        base.Initialize();
    }
}
