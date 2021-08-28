using System;

namespace TileGame.Character
{
    public abstract class Inventory<T>
    {
        private readonly T[] contents;
        private readonly int slotCount;

        public Inventory(int slotCount)
        {
            this.slotCount = slotCount;
            contents = new T [slotCount];
        }

        public bool SetContent(T entity, int slotNumber)
        {
            if (contents.Length < slotNumber) return false;

            contents[slotNumber] = entity;
            return true;
        }

        public T GetContent(int slotNumber)
        {
            if (slotCount == 0 || contents.Length < slotNumber) throw new ArgumentException("Parameter cannot be null");

            return contents[slotNumber];
        }
    }
}