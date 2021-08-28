namespace TileGame.Tiles
{
    public struct TileAssembly
    {
        public TileAssembly(string[] traversableTiles, string[] blockadeTiles)
        {
            TraversableTiles = traversableTiles;
            BlockadeTiles = blockadeTiles;
        }

        public string[] TraversableTiles { get; }
        public string[] BlockadeTiles { get; }
    }
}