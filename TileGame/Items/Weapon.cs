namespace TileGame.Items
{
    internal class Weapon : ItemBase
    {
        public Weapon(string name, string description, double price, float weight, string textureName,
            int strengthBonus) : base(name, description, price, weight, textureName, strengthBonus)
        {
        }

        public Weapon()
        {
            Name = "Weapon";
            Description =
                "A simple sword which inflicts consistent regular damage.";
        }
    }
}