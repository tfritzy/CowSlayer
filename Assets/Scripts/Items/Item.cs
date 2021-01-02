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
    public Effect PrimaryEffect;
    protected abstract Effect PrimaryEffectPrefab { get; }
    protected abstract List<Effect> SecondaryEffectPool { get; }
    protected abstract int NumSecondaryEffects { get; }
    public List<Effect> SecondaryEffects;
    public int Quantity;
    public virtual bool Stacks => false;

    /// <summary>
    /// Creates a new instance of this item. If effects are not passed, new effects will
    /// be rolled. This should be used for item creation. 
    /// </summary>
    public Item()
    {
        this.Id = GenerateId();
        Prefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Drops}/{Name.Replace(" ", "")}");
        this.SecondaryEffects = GenerateSecondaryEffects();
        this.PrimaryEffect = PrimaryEffectPrefab;
    }

    protected List<Effect> GenerateSecondaryEffects()
    {
        if (NumSecondaryEffects == 0)
        {
            return null;
        }

        List<Effect> effects = new List<Effect>();
        List<Effect> effectPoolCopy = new List<Effect>(this.SecondaryEffectPool);
        for (int i = 0; i < Math.Min(NumSecondaryEffects, SecondaryEffectPool.Count); i++)
        {
            int rollIndex = UnityEngine.Random.Range(0, effectPoolCopy.Count);
            effects.Add(effectPoolCopy[rollIndex]);
            effectPoolCopy.RemoveAt(rollIndex);
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

    public Item SplitStack(int newQuantity)
    {
        Item newStack = this.Duplicate();
        newStack.Quantity = newQuantity;
        this.Quantity -= newQuantity;
        return newStack;
    }

    public Item Duplicate()
    {
        Item newItem = (Item)this.MemberwiseClone();
        newItem.Id = GenerateId();
        return newItem;
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

        if (SecondaryEffects == null)
        {
            return;
        }

        foreach (Effect effect in SecondaryEffects)
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
        for (; i < SecondaryEffects?.Count; i++)
        {
            stats.Find($"Stat{i}").Find("Text").GetComponent<Text>().text = SecondaryEffects[i].ShortDescription;
        }

        for (; i < 5; i++)
        {
            stats.Find($"Stat{i}").gameObject.SetActive(false);
        }

        return itemDetails;
    }

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

    private string GenerateId()
    {
        return $"{Name}_{Guid.NewGuid().ToString("N")}";
    }
}