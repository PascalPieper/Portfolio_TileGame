using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Items;

namespace TileGame.Character
{
    public class ItemInventory : Level.Inventory<ItemBase>, IUpdate
    {
        public delegate void OnItemChanged();

        public ItemBase ArmorSlot;
        public ItemBase RingSlot;
        public ItemBase WeaponSlot;

        public ItemInventory(int maxSlots) : base(maxSlots)
        {
        }

        public void Update()
        {
            ImGui.Begin("Inventory Utility");
            ImGui.Columns(3);
            if (ImGui.Button("Sort by weight"))
            {
                var sortedItems = SortByWeight();
                Items.Clear();
                foreach (var item in sortedItems) Items.Add(item);
            }

            ImGui.NextColumn();
            if (ImGui.Button("Sort by name"))
            {
                var sortedItems = SortByName();
                Items.Clear();
                foreach (var item in sortedItems) Items.Add(item);
            }

            ImGui.NextColumn();
            if (ImGui.Button("Sort by price"))
            {
                var sortedItems = SortByPrice();
                Items.Clear();
                foreach (var item in sortedItems) Items.Add(item);
            }

            ImGui.End();

            DisplayEquipSlot("Trousers", ref ArmorSlot);

            DisplayEquipSlot("Weapon", ref WeaponSlot);

            DisplayEquipSlot("Ring", ref RingSlot);

            ImGui.Begin("Inventory");
            ImGui.Columns(2);
            for (var i = 0; i < Items.Count; i++)
            {
                ImGui.Text(Items[i].Name);
                ImGui.Text("Worth: " + Items[i].Price + "$");
                ImGui.Text("Weight: " + Math.Round(Items[i].Weight, 2));
                ImGui.Text("Bonus Strength: " + Items[i].StrengthBonus);
                ImGui.NextColumn();
                var nameNumber = i + 1;
                if (ImGui.Button("Equip Item " + nameNumber))
                {
                    switch (Items[i])
                    {
                        case Ring when RingSlot == null:
                            RingSlot = Items[i];
                            Items.Remove(Items[i]);
                            ItemChangeEvent?.Invoke();
                            break;
                        
                        case Weapon when WeaponSlot == null:
                            WeaponSlot = Items[i];
                            Items.Remove(Items[i]);
                            ItemChangeEvent?.Invoke();
                            break;
                        
                        case Armor when ArmorSlot == null:
                            ArmorSlot = Items[i];
                            Items.Remove(Items[i]);
                            ItemChangeEvent?.Invoke();
                            break;
                        
                        default:
                            Notifier.SetMessage("You already have an item of type " + Items[i].Name + " equipped.");
                            break;
                    }
                }


                if (ImGui.Button("Destroy Item " + nameNumber))
                {
                    Items.Remove(Items[i]);
                    ItemChangeEvent?.Invoke();
                }

                ImGui.NewLine();
                ImGui.NextColumn();
            }

            ImGui.End();
        }

        public event OnItemChanged ItemChangeEvent;

        private List<ItemBase> SortByWeight()
        {
            var sortedItems = Items.OrderBy(itemBase => itemBase.Weight).ToList();
            return sortedItems;
        }

        private List<ItemBase> SortByName()
        {
            var sortedItems = Items.OrderBy(itemBase => itemBase.Name).ToList();
            return sortedItems;
        }

        private List<ItemBase> SortByPrice()
        {
            var sortedItems = Items.OrderBy(itemBase => itemBase.Price).ToList();
            return sortedItems;
        }

        private void DisplayEquipSlot(string slotName, ref ItemBase item)
        {
            ImGui.Begin(slotName);

            if (item != null)
            {
                ImGui.Text(item.Name);
                ImGui.Text(item.Description);
                ImGui.Text("Worth: " + item.Price + "$");
                ImGui.Text("Weight: " + Math.Round(item.Weight, 2));
                ImGui.Text("Bonus Strength: " + item.StrengthBonus);

                if (ImGui.Button("Unequip " + item.Name))
                    if (AddItemToFront(item))
                    {
                        item = null;
                        ItemChangeEvent?.Invoke();
                    }
            }

            ImGui.End();
        }
    }
}