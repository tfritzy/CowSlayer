﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class ZoneGuardian : Cow
{
    public int Zone;

    protected override void OnDeath()
    {

        base.OnDeath();
    }
}

