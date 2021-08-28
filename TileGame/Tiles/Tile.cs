using System;
using SFML.Graphics;
using SFML.System;
using TileGame.Interfaces;
using TileGame.Level;
using TileGame.Pathfinding;
using TileGame.Tiles.Behavior;

namespace TileGame.Tiles
{
    public abstract class Tile : ITick, IDisposable
    {
        protected Tile(string name, CharacterEffectBehavior behavior, Color rectColor)
        {
            Name = name;
            TileRect = new RectangleShape();
            TileRect.FillColor = rectColor;
            Node = new Node(true, new Vector2f(0, 0), new Vector2i(0, 0));
        }

        protected Tile()
        {
            Name = "Default Tile";
            HighlightRect = new RectangleShape();
            TileRect = new RectangleShape();
            HighlightRect.FillColor = new Color(0, 0, 0, 0);
            Node = new Node(true, new Vector2f(0, 0), new Vector2i(0, 0));
        }

        public string Name { get; protected set; }

        public Node Node { get; set; }

        protected string BaseTextureName { get; set; }

        public RectangleShape TileRect { get; }

        public RectangleShape HighlightRect { get; }
        public TreasureChest TreasureChest { get; set; }
        public uint Identifier => 0;

        public virtual void Tick()
        {
        }

        public void Dispose()
        {
            TileRect?.Dispose();
            HighlightRect?.Dispose();
        }
    }
}