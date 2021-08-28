using System;
using TileGame.Items;
using TileGame.Utility.Random;

namespace TileGame.Game
{
    internal class ItemFactory : ReflectFactory<ItemBase>
    {
        public GameManager GameManager;

        public ItemFactory(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        public ItemFactory()
        {
            
        }

        public ItemBase CreateItem(string itemIdentifier)
        {
            try
            {
                var item = GetInstance(itemIdentifier);

                item.Price = RandomGenerator.RandomNumber(0, 255);
                item.Weight = RandomGenerator.RandomNumber(0.1f, 5);
                item.StrengthBonus = RandomGenerator.RandomNumber(1, 5);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in [ItemFactory.cs] - Specified Tile Name: " + itemIdentifier +
                                  " does not exist as derived type - " + e.Message);

                var item = GetInstance(nameof(Ring));
                //gameManager.AddGameObjectToLoop(item, item.Sprite);
                return item;
            }
        }
    }
}