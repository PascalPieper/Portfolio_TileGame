using SFML.Graphics;
using TileGame.Character;
using TileGame.Game;
using TileGame.Interfaces;

namespace TileGame.Tiles
{
    public class PoisonSwamp : Tile, ITraversable
    {
        public PoisonSwamp()
        {
            TileRect.Texture = ResourceManager.Instance.LoadTexture("resources/poison.png");
            Node.Walkable = true;
        }

        public void OnEnter(Player player)
        {
            HighlightRect.FillColor = Color.Transparent;
            player.Health -= 5;
        }

        public void OnExit()
        {
        }
    }
}