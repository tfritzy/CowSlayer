using System.Collections.Generic;
using UnityEngine;

public class WimpyCowDropTable : DropTable
{
    public override void SetValues()
    {
        DropChances = new Dictionary<Drop, int>
        {
            { new GoldDrop(10, 100), 10 },
            { new ItemDrop(new IronSword()), 5 },
            { new ItemDrop(new WoodenSword()), 5 },
            { new ItemDrop(new SteelSword()), 65 },
            { new ItemDrop(new ShoddyCrossbow()), 5 },
            { new ItemDrop(new Stick()), 5 },
            { new ItemDrop(new BasicStaff()), 5 },
        };

        base.SetValues();
    }
}