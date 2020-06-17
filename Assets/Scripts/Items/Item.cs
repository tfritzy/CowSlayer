using System;
using UnityEngine;

public abstract class Item
{
    public abstract string Name { get; }
    public string Id;
    public GameObject Prefab;

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

    public virtual void OnEquip() { }
    public virtual void OnUnequip() { }
}