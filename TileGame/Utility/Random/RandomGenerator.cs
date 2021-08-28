using System;

namespace TileGame.Utility.Random
{
    public static class RandomGenerator
    {
        private static readonly System.Random Random = new();
        private static readonly object SyncLock = new();

        public static float RandomNumber(float minRange, float maxRange)
        {
            var rand = new System.Random(Guid.NewGuid().GetHashCode());
            var val = (float)(rand.NextDouble() * (maxRange - minRange) + minRange);
            return val;
        }

        public static int RandomNumber(int minRange, int maxRange)
        {
            lock (SyncLock)
            {
            }

            return Random.Next(minRange, maxRange + 1);
        }

        public static uint RandomNumber(uint minRange, uint maxRange)
        {
            lock (SyncLock)
            {
            }

            var result = Math.Abs(Random.Next((int)minRange, (int)maxRange + 1));
            return (uint)result;
        }
    }
}