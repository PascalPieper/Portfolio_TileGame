namespace TileGame.Items
{
    internal class Armor : ItemBase
    {
        public Armor(string name, string description, double price, float weight, string textureName, int strengthBonus)
            : base(name, description, price, weight, textureName, strengthBonus)
        {
        }

        public Armor()
        {
            Name = "Trousers";
            Description = "Leggings of a nameless knight, who perished centuries ago.";
        }
    }
}