using System.Collections.Generic;
using UnityEngine;

public class WimpyCowDropTable : DropTable
{
    public override void SetValues()
    {
        DropChances = new Dictionary<Drop, int>
        {
            { new GoldDrop(10, 100), 10 },
            { new ItemDrop(new Crossbow()), 90 },
        };

        base.SetValues();
    }
}