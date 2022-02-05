using System.Collections.Generic;

public class IronSword : Sword
{
    public override string Name => "Iron Sword";
    public override ItemRarity Rarity => ItemRarity.Uncommon;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(3, 8, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => null;
    protected override int NumSecondaryEffects => 0;
}