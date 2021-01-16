using UnityEngine;

public class Charge : MeleeSkill
{
    public override string Name => "Charge";

    public override float Cooldown => 10f;

    public override bool CanAttackWhileMoving => true;

    public override int ManaCost => 0;

    public override SkillType Type => SkillType.Charge;

    public override float DamageModifier => 5f;

    public readonly float ChargeDistance = 10f;
    public readonly float ChargeWidth = 2f;

    public Charge(Character owner) : base(owner)
    {
    }

    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails)
    {
        Vector3 diffVector = attackTargetingDetails.Target.transform.position - attackTargetingDetails.Attacker.transform.position;
        diffVector.y = 0;
        Quaternion rotation = Quaternion.LookRotation(diffVector);
        Vector3 position = (attackTargetingDetails.Target.transform.position - attackTargetingDetails.Attacker.transform.position).normalized * (ChargeDistance / 2);
        position = attackTargetingDetails.Attacker.transform.position + position;
        GameObject inst = GameObject.Instantiate(Constants.Prefabs.Decals.LongRectangle);
        inst.transform.position = position;
        inst.transform.rotation *= rotation;
        inst.transform.localScale = new Vector3(ChargeWidth, 1f, ChargeDistance);
        inst.GetComponent<Decal>().Setup(10, 1);
    }
}