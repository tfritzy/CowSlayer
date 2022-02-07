using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Stick : Sword
{
    private ItemRarity rarity;
    public Stick(int level) : base(level)
    {
        this.rarity = RarityFromId(this.Id);
    }

    public override string Name => "Stick";
    public override ItemRarity Rarity => this.rarity;
}

