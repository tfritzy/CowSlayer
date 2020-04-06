public class GrassTile : TileData
{
    public override TileType Type { get { return TileType.Grass; } }
    public override bool IsPathable { get { return true; } }
}