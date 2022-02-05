using UnityEngine;
using System.Collections;

public class SmallManaRestore : ManaRestore
{
    private const string name = "Small mana restore";
    public override string Name => name;

    public SmallManaRestore(string id) : base(30, id) { }
}
