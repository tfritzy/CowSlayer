using System.Collections.Generic;

public class WoodenSword : Sword
{
    public override string Name => "Wooden Sword";
    public override ItemRarity Rarity => ItemRarity.Common;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(1, 5, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => null;
    protected override int NumSecondaryEffects => 0;
}