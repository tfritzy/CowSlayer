using System.Collections.Generic;
using UnityEngine;

public class WimpyCowDropTable : DropTable
{
    public override void SetValues()
    {
        DropChances = new Dictionary<Drop, int>
        {
            { new GoldDrop(1, 3), 50 },
            { new ItemDrop(new Stick()), 10 },
            { new ItemDrop(new IronPlatelegs()), 10 },
            { new ItemDrop(new NorthstarAmulet()), 10 },
            { new ItemDrop(new IceRing()), 10 },
            { new ItemDrop(new GoldRing()), 10 },
        };

        base.SetValues();
    }
}