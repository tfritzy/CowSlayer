using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

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
            ColorExtensions.Create(209, 209, 209)  // Grey
        },
        {
            ItemRarity.Uncommon,
            ColorExtensions.Create(145, 243, 140) // Light Green
        },
        {
            ItemRarity.Rare,
            ColorExtensions.Create(131, 182, 253) // Light blue
        },
        {
            ItemRarity.Exquisite,
            ColorExtensions.Create(253, 205, 84) // Light Orange
        },
        {
            ItemRarity.Legendary,
            ColorExtensions.Create(184, 108, 248) // Light Purple
        }
    };

    public Item()
    {
        this.Id = this.Name + Guid.NewGuid().ToString("N");
        Prefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Drops}/{Name.Replace(" ", "")}");
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
        return Resources.Load<Sprite>($"{Constants.FilePaths.Icons}/{this.Name.Replace(" ", "")}");
    }

    public Color GetRarityColor()
    {
        return RarityColors[this.Rarity];
    }

    public virtual void OnEquip() { }
    public virtual void OnUnequip() { }
}