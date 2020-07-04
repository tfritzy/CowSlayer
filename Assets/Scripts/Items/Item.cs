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
    public ItemEffect PrimaryEffect;
    protected abstract ItemEffect PrimaryEffectPrefab { get; }
    protected abstract List<ItemEffect> SecondaryEffectPool { get; }
    protected abstract int NumSecondaryEffects { get; }
    public List<ItemEffect> SecondaryEffects;

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
        this.SecondaryEffects = GenerateItemEffects();
        this.PrimaryEffect = PrimaryEffectPrefab;
        this.PrimaryEffect.RollRandomValue();
    }

    protected List<ItemEffect> GenerateItemEffects()
    {
        List<ItemEffect> effects = new List<ItemEffect>();
        List<ItemEffect> effectPoolCopy = new List<ItemEffect>(this.SecondaryEffectPool);
        for (int i = 0; i < Math.Min(NumSecondaryEffects, SecondaryEffectPool.Count); i++)
        {
            int rollIndex = UnityEngine.Random.Range(0, effectPoolCopy.Count);
            effects.Add(effectPoolCopy[rollIndex]);
            effectPoolCopy.RemoveAt(rollIndex);
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

    public Color GetDarkRarityColor()
    {
        Color color = GetRarityColor();
        color = color / 1.4f;
        color.a = 1;
        return color;
    }

    public virtual void ApplyEffects(Character character)
    {
        PrimaryEffect.Apply(character);
        foreach (ItemEffect effect in SecondaryEffects)
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

    public virtual GameObject ShowItemDetailsPage()
    {
        GameObject itemDetails = GameObject.Instantiate(Constants.Prefabs.ItemDetailsPage, Constants.Persistant.InteractableUI);
        Text title = itemDetails.transform.Find("TitleText").GetComponent<Text>();
        title.text = Name;
        title.color = GetRarityColor();
        itemDetails.transform.Find("ItemIcon").GetComponent<Image>().color = GetRarityColor();
        itemDetails.transform.Find("ItemIcon").Find("Icon").GetComponent<Image>().sprite = GetIcon();
        itemDetails.transform.Find("Background").GetComponent<Image>().color = Constants.UI.Colors.Base;
        itemDetails.transform.Find("Outline").GetComponent<Image>().color = GetRarityColor();
        itemDetails.transform.Find("PrimaryStatDescription").GetComponent<Text>().text = this.PrimaryEffect.ShortDescription;
        Transform stats = itemDetails.transform.Find("Stats");

        int i = 0;
        for (; i < SecondaryEffects.Count; i++)
        {
            stats.Find($"Stat{i}").Find("Text").GetComponent<Text>().text = SecondaryEffects[i].ShortDescription;
        }

        for (; i < 5; i++)
        {
            stats.Find($"Stat{i}").gameObject.SetActive(false);
        }

        return itemDetails;
    }
}