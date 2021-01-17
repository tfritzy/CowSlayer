﻿using UnityEngine;
using System.Collections;
using System;

public class Dummy : Cow
{
    public override CowType CowType => CowType.WimpyCow;

    public Allegiance EnemyOverride;
    public Allegiance AllegianceOverride;
    public override float ManaRegenPerMinute => 50f;

    public override void Initialize()
    {
        this.Name = "Dummy " + Guid.NewGuid().ToString("N").Substring(0, 8);
        this.DropTable = new WimpyCowDropTable();
        base.Initialize();
        this.Enemies = new System.Collections.Generic.HashSet<Allegiance> { EnemyOverride };
        this.Allegiance = AllegianceOverride;
    }

    protected override void SetInitialStats()
    {
        this.Health = 100000;
        this.MaxHealth = Health;
        this.Damage = 2;
        this.AttackSpeedPercent = 1;
        this.TargetFindRadius = 10;
        this.RangedAttackRange = 2f;
        this.MeleeAttackRange = 10f;
        this.MovementSpeed = 0f;
        this.PrimarySkill = new FireBolt();
    }

    public override void Attack()
    {
        base.Attack();
        float lastLastAttackTime = this.PrimarySkill.LastAttackTime;
        this.PrimarySkill = new FireBolt();
        this.PrimarySkill.LastAttackTime = lastLastAttackTime;
    }
}
