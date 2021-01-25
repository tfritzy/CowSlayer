using System.Collections.Generic;
using UnityEngine;

public class WimpyCowDropTable : DropTable
{
    public override void SetValues()
    {
        DropChances = new Dictionary<Drop, int>
        {
            { new GoldDrop(10, 100), 30 },
            { new ItemDrop(new IronSword()), 10 },
            { new ItemDrop(new SteelSword()), 10 },
            { new ItemDrop(new WoodenSword()), 10 },
            { new ArrowDrop(1, 10), 10 },
            { new ItemDrop(new ShoddyCrossbow()), 10 },
            { new ItemDrop(new Stick()), 10 },
            { new ItemDrop(new BasicStaff()), 10 },
        };

        base.SetValues();
    }
}