using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using System.Linq;

public abstract class Item
{
    public abstract string Name { get; }
    public abstract ItemRarity Rarity { get; }
    public string Id;
    public GameObject Prefab;
    public virtual int Price => 8; // TODO: calculate value.
    public List<StatModifier> PrimaryAttributes;
    protected abstract List<StatModifier> GeneratePrimaryAttributes();
    public List<StatModifier> SecondaryAttributes;
    protected abstract Func<string, float, StatModifier>[] SecondaryAttributePool { get; }
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
    public int Level { get; protected set; }
    public float BasePower => this.Level * 10;
    public float PowerPerAttribute => this.Level * 5;

    /// <summary>
    /// Creates a new instance of this item. If effects are not passed, new effects will
    /// be rolled. This should be used for item creation. 
    /// </summary>
    public Item()
    {
        this.Id = Helpers.GenerateId(ID_PREFIX);
        this.PrimaryAttributes = GeneratePrimaryAttributes();
        this.SecondaryAttributes = GenerateSecondaryEffects();

        if (this.HasInstantiation)
        {
            Prefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Equipment}/{Name.Replace(" ", "")}");
        }
    }

    protected List<StatModifier> GenerateSecondaryEffects()
    {
        int numSecondaryEffects = GetNumAttributes(this.Rarity);
        List<StatModifier> effects = new List<StatModifier>();
        List<int> secondaryEffectIndexOptions = Enumerable.Range(0, SecondaryAttributePool.Length).ToList();
        System.Random random = new System.Random(this.Id.GetHashCode());

        for (int i = 0; i < numSecondaryEffects; i++)
        {
            if (secondaryEffectIndexOptions.Count == 0)
            {
                throw new System.Exception($"Item {this.Name} doesn't have a large enough secondary pool.");
            }

            float power = RollPowerForAttribute(this.Id);
            int index = secondaryEffectIndexOptions[random.Next(secondaryEffectIndexOptions.Count)];
            secondaryEffectIndexOptions.Remove(index);
            effects.Add(SecondaryAttributePool[index](this.Id, power));
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
        foreach (StatModifier attribute in this.PrimaryAttributes)
        {
            attribute.Apply(character);
        }

        foreach (StatModifier attribute in SecondaryAttributes)
        {
            attribute.Apply(character);
        }
    }

    public virtual void OnEquip(Character bearer)
    {
        this.ApplyEffects(bearer);
    }

    public virtual void OnUnEquip(Character bearer)
    {
        foreach (StatModifier attribute in this.PrimaryAttributes)
        {
            bearer.RemoveStatModifier(attribute);
        }

        foreach (StatModifier modifier in this.SecondaryAttributes)
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
        itemDetails.transform.Find("PrimaryStatDescription").GetComponent<Text>().text = this.PrimaryAttributes.First().ShortDescription; // TODO: Make UI handle multiple primaries.
        Transform stats = itemDetails.transform.Find("Stats");
        int i = 0;
        for (; i < SecondaryAttributes?.Count; i++)
        {
            stats.Find($"Stat{i}").Find("Text").GetComponent<Text>().text = SecondaryAttributes[i].ShortDescription;
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

    private const int NUM_ROLL_SLOTS = 10000;
    protected ItemRarity RarityFromId(string id)
    {
        // 40% common, 30% Uncommon, 20% rare, 10% exquisite
        // Legendary items are their own unique items, and cannot be rolled here.
        int roll = id.GetHashCode() % NUM_ROLL_SLOTS;
        if (roll < 4000)
        {
            return ItemRarity.Common;
        }
        else if (roll < 7000)
        {
            return ItemRarity.Uncommon;
        }
        else if (roll < 9000)
        {
            return ItemRarity.Rare;
        }
        else
        {
            return ItemRarity.Exquisite;
        }
    }

    private static int GetNumAttributes(ItemRarity rarity)
    {
        switch (rarity)
        {
            case (ItemRarity.Common):
                return 0;
            case (ItemRarity.Uncommon):
                return 1;
            case (ItemRarity.Rare):
                return 2;
            case (ItemRarity.Exquisite):
                return 3;
            case (ItemRarity.Legendary):
                return 4;
            default:
                throw new System.Exception("Unknown rarity");
        }
    }

    protected static float RollPowerForAttribute(string itemId)
    {
        System.Random random = new System.Random(itemId.GetHashCode());
        float power = (float)random.NextDouble() + .5f;

        // Be nice and make pretty numbers;
        if (power >= 1.45f)
        {
            power = 1.5f;
        }

        return power;
    }
}