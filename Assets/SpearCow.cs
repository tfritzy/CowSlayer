using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCow : Cow
{

    private CowType cowType = CowType.SpearThrower;
    public override CowType CowType => cowType;

    public override void Initialize()
    {
        this.Name = "Spear Cow";
        this.DropTable = new BasicCowDropTable();
        base.Initialize();
        this.PrimarySkill = new SpearThrow(this);
    }
}
