using System.Collections.Generic;
using UnityEngine;

public class WimpyCowDropTable : DropTable
{
    public override void SetValues()
    {
        DropChances = new Dictionary<Drop, int>
        {
            { new GoldDrop(10, 100), 90 },
            { new ItemDrop(new Stick()), 10 },
        };

        base.SetValues();
    }
}