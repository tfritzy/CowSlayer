using UnityEngine;
using System.Collections;

public class SmallHealthRestore : HealthRestore
{
    private const string name = "Small health restore";
    public override string Name => name;

    public SmallHealthRestore(string id) : base(30, id) { }
}
