using System;
using System.Collections.Generic;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Items;

namespace TileGame.Character
{
    public class Player : Char, IMove, IHealth
    {
        public delegate void DeathHandler();

        private int _health = 100;

        public Player(ItemInventory itemInventory) : base(itemInventory)
        {
            Sprite = new Sprite();
            Sprite.Texture = ResourceManager.Instance.LoadTexture("resources/player.png");
            Sprite.Scale = new Vector2f(0.65f, 0.65f);
            itemInventory.ItemChangeEvent += Validate;
        }

        public int Health
        {
            get => _health;
            set
            {
                if (0 >= value)
                {
                    OnPlayerDeath();
                    _health = 0;
                }
                else
                {
                    _health = value;
                }
            }
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public bool MoveUp()
        {
            Sprite.Position += new Vector2f(0, -8);
            return true;
        }

        public bool MoveDown()
        {
            Sprite.Position += new Vector2f(0, 8);
            return true;
        }

        public bool MoveLeft()
        {
            Sprite.Position += new Vector2f(-8, 0);
            return true;
        }

        public bool MoveRight()
        {
            Sprite.Position += new Vector2f(8, 0);
            return true;
        }

        public event DeathHandler PlayerDeathEvent;


        private void ResetStats()
        {
            Strength = StartStrength;
            MaxWeight = StartMaxWeight;
            CurrentWeight = 0;
        }

        public void Validate()
        {
            var equipped = new List<ItemBase>
                { ItemInventory.ArmorSlot, ItemInventory.RingSlot, ItemInventory.WeaponSlot };

            ResetStats();
            foreach (var item in ItemInventory.Items) CurrentWeight += item.Weight;

            foreach (var item in equipped)
                if (item != null)
                {
                    Strength += item.StrengthBonus;
                    CurrentWeight += item.Weight;
                    MaxWeight += Strength * StrengthMulti;
                }


            CanMove = !(CurrentWeight > MaxWeight);
            if (!CanMove) Notifier.SetMessage("You have too little strength to carry this weight.");
        }


        public override void Update()
        {
            base.Update();
            ImGui.Begin("Player Stats");
            ImGui.SetWindowFontScale(2);

            ImGui.Text("Current Health: " + Health);
            ImGui.Text("Strength: " + Strength);
            ImGui.Text("Current Weight: " + Math.Round(CurrentWeight, 2));
            ImGui.Text("Max Weight: " + MaxWeight);

            ImGui.End();
        }


        protected virtual void OnPlayerDeath()
        {
            CanMove = false;
            PlayerDeathEvent?.Invoke();
            Notifier.SetMessage("How did you manage to die in this kind of game?", 8f);
        }
    }
}