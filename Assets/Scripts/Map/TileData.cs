using System;
using System.Collections.Generic;

public abstract class TileData
{
    public abstract TileType Type { get; }
    public abstract bool IsPathable { get; }

    private static Dictionary<TileType, TileData> _tileDataMap;
    public static Dictionary<TileType, TileData> TileDataMap
    {
        get
        {
            if (_tileDataMap == null)
            {
                _tileDataMap = new Dictionary<TileType, TileData>()
                {
                    { TileType.Grass, new GrassTile() }
                };
            }
            return _tileDataMap;
        }
    }
}