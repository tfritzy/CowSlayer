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
    public virtual int Price => 10;
    protected abstract List<ItemEffect> EffectPool { get; }
    protected abstract int NumEffects { get; }
    public List<ItemEffect> Effects;

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

    /// <summary>
    /// Creates a new instance of this item. If effects are not passed, new effects will
    /// be rolled. This should be used for item creation. 
    /// </summary>
    public Item()
    {
        this.Id = this.Name + Guid.NewGuid().ToString("N");
        Prefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Drops}/{Name.Replace(" ", "")}");
        this.Effects = GenerateItemEffects();
    }

    protected List<ItemEffect> GenerateItemEffects()
    {
        List<ItemEffect> effects = new List<ItemEffect>();
        List<ItemEffect> effectPoolCopy = new List<ItemEffect>(this.EffectPool);
        for (int i = 0; i < NumEffects; i++)
        {
            int rollIndex = UnityEngine.Random.Range(0, effectPoolCopy.Count);
            effects.Add(effectPoolCopy[rollIndex]);
            effectPoolCopy.RemoveAt(i);
        }

        foreach (ItemEffect effect in effects)
        {
            effect.RollRandomValue();
        }

        return effects;
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

    public virtual void ApplyEffects(Character character)
    {
        foreach (ItemEffect effect in Effects)
        {
            effect.Apply(character);
        }
    }

    public virtual void OnEquip(Character bearer) 
    {
        bearer.RecalculateItemEffects();
    }

    public virtual void OnUnequip(Character bearer) 
    {
        bearer.RecalculateItemEffects();
    }
}