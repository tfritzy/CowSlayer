using UnityEngine;

public class FlameSprite : PassiveSkill
{
    public override string Name => "Flame Sprite";
    public override float Cooldown => 4f;
    public override bool CanAttackWhileMoving => true;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.FlameSprite;
    public override bool IsPassive => true;
    public override float DamageModifier => 1f;
    protected override string IconFilePath => $"{Constants.FilePaths.Icons}/FlameSprite";
    private GameObject SpriteBody;
    private string SpriteBodyPath = $"{Constants.FilePaths.Prefabs.Skills}/FlameSpriteBody";

    public override void ApplyEffect(AttackTargetingDetails attackTargetingDetails)
    {
        if (SpriteBody == null)
        {
            SpriteBody = GameObject.Instantiate(Resources.Load<GameObject>(SpriteBodyPath), attackTargetingDetails.Attacker.transform.position, new Quaternion(), attackTargetingDetails.Attacker.transform);
        }

        Constants.Persistant.PlayerScript.Mana += 10 * Level;
        CreatePrefab(attackTargetingDetails);
    }

    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails)
    {
        Vector3 position = attackTargetingDetails.Attacker.transform.position;
        position.y = Constants.WorldProperties.GroundLevel;
        GameObject inst = GameObject.Instantiate(Prefab, position, new Quaternion(), attackTargetingDetails.Attacker.transform);
        GameObject.Destroy(inst, 5f);
    }
}