using SFML.Graphics;
using TileGame.Character;
using TileGame.Game;
using TileGame.Interfaces;

namespace TileGame.Tiles
{
    public class ExitTile : Tile, ITraversable
    {
        public ExitTile()
        {
            TileRect.Texture = ResourceManager.Instance.LoadTexture("resources/exitpoint.png");
        }

        public void OnEnter(Player player)
        {
            HighlightRect.FillColor = Color.Transparent;
            Notifier.SetMessage("Final Tile reached. Well done!");
        }

        public void OnExit()
        {
        }
    }
}