using UnityEngine;
using System.Collections;

public class SmallHealthRestore : HealthRestore
{
    public override int Value => 5;
    public override string Name => "Small Health Restore";
}
