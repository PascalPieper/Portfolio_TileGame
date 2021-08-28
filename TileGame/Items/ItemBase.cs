using SFML.Graphics;
using TileGame.Interfaces;

namespace TileGame.Items
{
    public class ItemBase : ITick
    {
        private float _weight;

        public int StrengthBonus;

        public ItemBase(string name, string description, double price, float weight, string textureName,
            int strengthBonus)
        {
            Name = name;
            Description = description;
            Price = price;
            _weight = weight;
            StrengthBonus = strengthBonus;
        }

        public ItemBase()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Sprite Sprite { get; set; } = null;

        public float Weight
        {
            get => _weight;
            set
            {
                if (value <= 0)
                {
                    _weight = 0.1f;
                    return;
                }

                _weight = value;
            }
        }

        public uint Identifier { get; } = 0;

        public void Tick()
        {
        }


        public void Equip()
        {
        }
    }
}