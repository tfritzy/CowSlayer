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
        };

        base.SetValues();
    }
}