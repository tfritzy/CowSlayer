using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MeleeSkill
{

    private string name = "Punch";
    public override string Name => name;

    private float cooldown = 2f;
    public override float Cooldown => cooldown;

    private bool canAttackWhileMoving = false;
    public override bool CanAttackWhileMoving => canAttackWhileMoving;

    private int manaCost = 0;
    public override int ManaCost => manaCost;

    private SkillType type = SkillType.Punch;
    public override SkillType Type => type;

    private float damageModifier = 1f;
    public override float DamageModifier => damageModifier;
    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails) { }

    private float range = .1f;
    public override float Range => range;

    public Punch(Character bearer) : base(bearer)
    {
    }
}
