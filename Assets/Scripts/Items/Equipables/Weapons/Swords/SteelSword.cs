using System.Collections.Generic;

public class SteelSword : Sword
{
    public override string Name => "Steel Sword";

    public override ItemRarity Rarity => ItemRarity.Uncommon;

    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(8, 12, this.Id);

    protected override List<StatModifier> SecondaryAttributePool => null;

    protected override int NumSecondaryEffects => 0;
}