using SFML.Graphics;
using TileGame.Interfaces;
using TileGame.Pathfinding;

namespace TileGame.Character
{
    public class Char : IUpdate, ITick
    {
        protected const int StartStrength = 0;
        protected const int StartMaxWeight = 5;
        protected const int StartHealth = 100;
        public Node OccupiedNode;

        public Char(ItemInventory itemInventory)
        {
            ItemInventory = itemInventory;
        }

        public ItemInventory ItemInventory { get; set; }
        public int Strength { get; set; } = 0;
        public float StrengthMulti { get; } = 3f;
        protected float MaxWeight { get; set; } = 5f;
        public float CurrentWeight { get; protected set; } = 0;

        public Sprite Sprite { get; set; }

        public bool CanMove { get; set; } = true;

        public uint Identifier { get; set; } = 0;

        public virtual void Tick()
        {
        }

        public virtual void Update()
        {
            ItemInventory.Update();
        }

        public virtual void Reconstruct()
        {
            MaxWeight = Strength * StrengthMulti;
        }
    }
}