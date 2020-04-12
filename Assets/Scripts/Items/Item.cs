using UnityEngine;

public abstract class Item
{
    public abstract string Name { get; }
    public override string ToString() 
    {
        return Name;
    }

}