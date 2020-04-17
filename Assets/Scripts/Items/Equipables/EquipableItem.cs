using UnityEngine;
using System.Collections;

public abstract class EquipableItem : Item
{
    public abstract ItemWearLocations PlaceWorn { get; }
}
