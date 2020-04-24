using System.Collections.Generic;

public class ItemWearLocations
{
    public enum SlotType {
        Head,
        Chest,
        Bracer,
        Legs,
        Boots,
        Ring,
        Amulet
    }
    public static readonly Dictionary<SlotType, int[]> Slots = new Dictionary<SlotType, int[]>() {
        { SlotType.Head, new int[] {0} },
        { SlotType.Chest, new int[] {1} },
        { SlotType.Bracer, new int[] {2, 3} },
        { SlotType.Legs, new int[] {4} },
        { SlotType.Boots, new int[] {5} },
        { SlotType.Ring, new int[] {6,7} },
        { SlotType.Amulet, new int[] {8} }
    };
}