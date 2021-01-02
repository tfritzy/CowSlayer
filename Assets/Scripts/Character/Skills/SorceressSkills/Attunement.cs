using UnityEngine;

public class Attunement : PassiveSkill
{
    public override string Name => "Attunement";
    public override float Cooldown => 10f;
    public override bool CanAttackWhileMoving => true;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.Attunement;
    public override bool IsPassive => true;
    public override float DamageModifier => 0f;
    protected override string IconFilePath => $"{Constants.FilePaths.Icons}/Attunement";

    public override void ApplyEffect(AttackTargetingDetails attackTargetingDetails)
    {
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