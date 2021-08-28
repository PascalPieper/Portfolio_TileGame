using SFML.Graphics;
using TileGame.Character;
using TileGame.Game;
using TileGame.Interfaces;

namespace TileGame.Tiles
{
    internal class Grass : Tile, ITraversable, IOccupiable
    {
        public Grass()
        {
            TileRect.Texture = ResourceManager.Instance.LoadTexture("resources/grass.png");
            Node.Walkable = true;
        }

        public bool Occupied { get; set; } = false;

        public void OnOccupy()
        {
        }

        public void OnEnter(Player player)
        {
            HighlightRect.FillColor = Color.Transparent;
        }

        public void OnExit()
        {
        }
    }

    internal interface IOccupiable
    {
        bool Occupied { get; }
        void OnOccupy();
    }
}