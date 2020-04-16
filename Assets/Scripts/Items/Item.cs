using System;
using UnityEngine;

public abstract class Item
{
    public abstract string Name { get; }
    public string Id;

    public Item()
    {
        this.Id = this.Name + Guid.NewGuid().ToString("N");
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
        return Resources.Load<Sprite>($"{Constants.FilePaths.Icons}/{this.IconName}");
    }

    public abstract string IconName { get; }
}