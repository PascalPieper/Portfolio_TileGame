using SFML.Graphics;
using SFML.System;
using TileGame.Character;
using TileGame.Game;
using TileGame.Items;

namespace TileGame.Level
{
    public class TreasureChest : IOccupy
    {
        public TreasureChest()
        {
            Sprite = new Sprite();
            Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/chest.png");
            Sprite.Scale = new Vector2f(0.65f, 0.65f);
        }

        public bool IsUsed { get; private set; }
        public Sprite Sprite { get; set; }

        public ItemBase HoldItem { get; set; } = null;

        public void GiveItem(Player player)
        {
            player.ItemInventory.AddItemToFront(HoldItem);
        }


        public ItemBase Open()
        {
            IsUsed = true;
            Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/chest_open.png");
            return HoldItem;
        }
    }

    public interface IOccupy
    {
        void GiveItem(Player player);
    }
}