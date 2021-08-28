namespace TileGame.Items
{
    public struct ItemAssembly
    {
        public ItemAssembly(string[] spawnableItems, float spawnFrequency, bool spawnPlayerWithItems, int playerStartItemAmount)
        {
            SpawnableItems = spawnableItems;
            SpawnFrequency = spawnFrequency;
            SpawnPlayerWithItems = spawnPlayerWithItems;
            PlayerStartItemAmount = playerStartItemAmount;
        }

        public string[] SpawnableItems { get; }
        public float SpawnFrequency { get; }
        
        public bool SpawnPlayerWithItems { get; }
        public int PlayerStartItemAmount { get; }
    }
}