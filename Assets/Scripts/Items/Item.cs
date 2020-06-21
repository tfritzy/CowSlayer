using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item
{
    public abstract string Name { get; }
    public abstract ItemRarity Rarity { get; }
    public string Id;
    public GameObject Prefab;

    protected readonly Dictionary<ItemRarity, Color> RarityColors = new Dictionary<ItemRarity, Color>
    {
        {
            ItemRarity.Common,
            Color.white
        },
        {
            ItemRarity.Uncommon,
            Color.green
        },
        {
            ItemRarity.Rare,
            Color.blue
        },
        {
            ItemRarity.Exquisite,
            Color.yellow
        },
        {
            ItemRarity.Legendary,
            Color.magenta
        }
    };

    public Item()
    {
        this.Id = this.Name + Guid.NewGuid().ToString("N");
        Prefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Weapons}/{Name}");
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    public Sprite GetIcon()
    {
        return Resources.Load<Sprite>($"{Constants.FilePaths.Icons}/{this.Name}");
    }

    public Color GetRarityColor()
    {
        return RarityColors[this.Rarity];
    }

    public virtual void OnEquip() { }
    public virtual void OnUnequip() { }
}