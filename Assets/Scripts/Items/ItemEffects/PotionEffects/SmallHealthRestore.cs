using UnityEngine;
using System.Collections;

public class SmallHealthRestore : HealthRestore
{
    public override int Value => 30;
    public override string Name => "Small Health Restore";
}
