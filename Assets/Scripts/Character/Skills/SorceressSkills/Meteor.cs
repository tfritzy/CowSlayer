﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : RangedSkill
{
    public int GroundFireCount => 8;
    public int GroundFireDamage => 1;
    public float GroundFireDuration => 2f;
    public override string Name => "Meteor";
    public override float Cooldown => 3f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 30;
    public override SkillType Type => SkillType.Meteor;

    protected override float MovementSpeed => 35f;
    protected override Vector3 ProjectileStartPositionOffset => new Vector3(0f, 30f, -10f);
    protected override float ExplosionRadius => 3f;
    protected override string IconFilePath => $"{Constants.FilePaths.Icons}/DaggerIcon";
    public override Dictionary<SkillType, int> UnlockDependsOn => new Dictionary<SkillType, int>()
    {
        {
            SkillType.Fireball,
            3
        },
    };

    public override bool IsCollisionTarget(Character attacker, GameObject collision)
    {
        return collision.CompareTag(Constants.Tags.Ground);
    }

    protected override void CreateGroundEffects(Character attacker, Vector3 position)
    {
        List<GameObject> fires = SpawnObjectsInCircle(Constants.Prefabs.GroundFire, GroundFireCount, position, (float)GroundFireCount / 6f);
        foreach (GameObject fire in fires)
        {
            fire.GetComponent<GroundFire>().Setup(GroundFireDamage, GroundFireDuration, attacker);
        }
    }
}
