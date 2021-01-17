using UnityEngine;

public class FlameSprite : PassiveSkill
{
    public override string Name => "Flame Sprite";
    public override float Cooldown => 4f;
    public override bool CanAttackWhileMoving => true;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.FlameSprite;
    public override float DamageModifier => 1f + Level * .25f;
    private GameObject SpriteBody;
    private string SpriteBodyPath = $"{Constants.FilePaths.Prefabs.Skills}/FlameSpriteBody";

    public override bool Activate(Character attacker, AttackTargetingDetails targetingDetails)
    {
        if (base.Activate(attacker, targetingDetails) == false)
        {
            return false;
        }

        if (targetingDetails.Target == null)
        {
            return false;
        }

        return true;
    }

    public override void ApplyEffect(AttackTargetingDetails attackTargetingDetails)
    {
        if (SpriteBody == null)
        {
            SpriteBody = GameObject.Instantiate(Resources.Load<GameObject>(SpriteBodyPath), attackTargetingDetails.Attacker.transform.position, new Quaternion(), attackTargetingDetails.Attacker.transform);
        }

        if (attackTargetingDetails.Target != null)
        {
            CreatePrefab(attackTargetingDetails);
        }
    }

    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails)
    {
        Vector3 position = SpriteBody.transform.position;
        GameObject projectile = GameObject.Instantiate(Prefab, position, new Quaternion(), attackTargetingDetails.Attacker.transform);
        DirectProjectile(projectile, attackTargetingDetails, 10f);
    }
}