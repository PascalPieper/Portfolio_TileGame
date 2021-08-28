using System;
using System.Collections.Generic;
using TileGame.Game;

namespace TileGame.Level
{
    public class Inventory<T>
    {
        private readonly List<T> _items;

        public Inventory(int maxSlots)
        {
            MaxSlots = maxSlots;
            _items = new List<T>(maxSlots);
        }

        public int MaxSlots { get; set; }

        public List<T> Items
        {
            get => _items;
            set
            {
                if (value.Count == 0)
                    return;
                if (value.Count > MaxSlots)
                    throw new Exception(GetType().Name +
                                        " The passed List surpasses the maximum Item limit set be the MaxSlots variable.");

                Items = value;
            }
        }

        public T GetItemAtIndex(int index)
        {
            return Items[index];
        }

        public bool AddItemToFront(T item)
        {
            if (Items.Count < MaxSlots)
            {
                Items.Add(item);
                return true;
            }

            Notifier.SetMessage("There's no space left in the Inventory. Discard some Items first.");

            return false;
        }
    }
}