using System.Collections.Generic;

public class BasicStaff : Staff
{
    public override string Name => "Basic Staff";
    public override ItemRarity Rarity => ItemRarity.Common;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(2, 4, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => null;
    protected override int NumSecondaryEffects => 0;
}