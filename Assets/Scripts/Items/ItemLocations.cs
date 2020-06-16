using System.Collections.Generic;

public class ItemWearLocations
{
    public enum SlotType {
        Head,
        Chest,
        Legs,
        Boots,
        Ring,
        Amulet,
        MainHand,
        OffHand
    }

    public static readonly Dictionary<SlotType, int[]> Slots = new Dictionary<SlotType, int[]>()
    {
        { SlotType.Head, new int[] {0} },
        { SlotType.Amulet, new int[] {1} },
        { SlotType.Ring, new int[] {2,4} },
        { SlotType.Chest, new int[] {3} },
        { SlotType.MainHand, new int[] {5} },
        { SlotType.Legs, new int[] {6} },
        { SlotType.OffHand, new int[] {7} },
        { SlotType.Boots, new int[] {8} },
    };
}