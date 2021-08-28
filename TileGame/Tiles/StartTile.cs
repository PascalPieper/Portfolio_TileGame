using TileGame.Character;
using TileGame.Game;
using TileGame.Interfaces;

namespace TileGame.Tiles
{
    public class StartTile : Tile, ITraversable
    {
        public StartTile()
        {
            TileRect.Texture = ResourceManager.Instance.LoadTexture("resources/spawnpoint.png");
        }

        public void OnEnter(Player player)
        {
        }

        public void OnExit()
        {
        }
    }
}