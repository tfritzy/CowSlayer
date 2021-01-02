
using System.Collections.Generic;

public class SmallHealthPotion : HealthPotion
{
    public override string Name => "Small Health Potion";
    protected override Effect PrimaryEffectPrefab => new SmallHealthRestore();
}