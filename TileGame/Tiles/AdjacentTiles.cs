namespace TileGame.Tiles
{
    /// <summary>
    ///     Holds references to tiles that count as directionally adjacent
    ///     North, East, South, West
    /// </summary>
    public class AdjacentTiles
    {
        public AdjacentTiles(Tile north, Tile east, Tile south, Tile west)
        {
            North = north;
            East = east;
            South = south;
            West = west;
        }

        public Tile North { get; }
        public Tile East { get; }
        public Tile South { get; }
        public Tile West { get; }
    }
}