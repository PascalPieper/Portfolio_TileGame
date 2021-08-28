namespace TileGame.Items
{
    internal class Ring : ItemBase
    {
        public Ring(string name, string description, double price, float weight, string textureName, int strengthBonus)
            : base(name, description, price, weight, textureName, strengthBonus)
        {
        }

        public Ring()
        {
            Name = "Ring";
            Description = "Ring made of an ancient red jewel.";
        }
    }
}