using UnityEngine;
using System.Collections;

public abstract class EquipableItem : Item
{
    public abstract ItemWearLocations.SlotType PlaceWorn { get; }
}
