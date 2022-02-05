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
    public abstract StatModifier PrimaryAttribute { get; }
    protected abstract List<StatModifier> SecondaryAttributePool { get; }
    protected abstract int NumSecondaryEffects { get; }
    public List<StatModifier> SecondaryEffects;
    public int Quantity;
    public virtual bool Stacks => false;
    protected virtual List<Tuple<int, string>> Icons
    {
        get
        {
            return new List<Tuple<int, string>> { new Tuple<int, string>(1, Name) };
        }
    }
    public virtual bool HasInstantiation => true;
    private const string ID_PREFIX = "Item";

    /// <summary>
    /// Creates a new instance of this item. If effects are not passed, new effects will
    /// be rolled. This should be used for item creation. 
    /// </summary>
    public Item()
    {
        this.Id = Helpers.GenerateId(ID_PREFIX);
        this.SecondaryEffects = GenerateSecondaryEffects();

        if (this.HasInstantiation)
        {
            Prefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Equipment}/{Name.Replace(" ", "")}");
        }
    }

    protected List<StatModifier> GenerateSecondaryEffects()
    {
        if (NumSecondaryEffects == 0)
        {
            return new List<StatModifier>();
        }

        List<StatModifier> effects = new List<StatModifier>();
        List<StatModifier> effectPoolCopy = new List<StatModifier>(this.SecondaryAttributePool);
        System.Random random = new System.Random(this.Id.GetHashCode());
        for (int i = 0; i < Math.Min(NumSecondaryEffects, SecondaryAttributePool.Count); i++)
        {
            int rollIndex = random.Next(0, effectPoolCopy.Count);
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
        newItem.Id = Helpers.GenerateId(ID_PREFIX);
        return newItem;
    }

    public Sprite GetIcon()
    {
        int iconIndex = 1;
        while (iconIndex < Icons.Count && Icons[iconIndex].Item1 <= Quantity)
        {
            iconIndex += 1;
        }

        return Constants.Icons[Icons[iconIndex - 1].Item2.Replace(" ", "")];
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
        PrimaryAttribute.ApplyModifier(character);

        if (SecondaryEffects == null)
        {
            return;
        }

        foreach (StatModifier effect in SecondaryEffects)
        {
            effect.ApplyModifier(character);
        }
    }

    public virtual void OnEquip(Character bearer)
    {
        bearer.AddStatModifier(this.PrimaryAttribute);

        foreach (StatModifier modifier in this.SecondaryEffects)
        {
            bearer.AddStatModifier(modifier);
        }
    }

    public virtual void OnUnEquip(Character bearer)
    {
        bearer.RemoveStatModifier(this.PrimaryAttribute);

        foreach (StatModifier modifier in this.SecondaryEffects)
        {
            bearer.RemoveStatModifier(modifier);
        }
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
        itemDetails.transform.Find("PrimaryStatDescription").GetComponent<Text>().text = this.PrimaryAttribute.ShortDescription;
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
}