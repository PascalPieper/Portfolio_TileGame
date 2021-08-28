using TileGame.Game;

namespace TileGame.Tiles
{
    internal class Mountains : Tile
    {
        public Mountains()
        {
            TileRect.Texture = ResourceManager.Instance.LoadTexture("resources/mountains.png");
            Node.Walkable = false;
        }
    }
}