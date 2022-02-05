using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowAttack : RangedSkill
{

    private string name = "CrossbowAttack";
    public override string Name => name;

    private float cooldown = 3f;
    public override float Cooldown => cooldown;

    private bool canAttackWhileMoving = false;
    public override bool CanAttackWhileMoving => canAttackWhileMoving;

    private int manaCost = 0;
    public override int ManaCost => manaCost;

    private SkillType type = SkillType.CrossbowAttack;
    public override SkillType Type => type;

    private float damageModifier = 2f;
    public override float DamageModifier => damageModifier;
    protected override float ProjectileSpeed => 20f;
    protected override Item Ammo => new Arrow();

    private float range = 8f;
    public override float Range => range;

    public CrossbowAttack(Character bearer) : base(bearer)
    {
    }
}
