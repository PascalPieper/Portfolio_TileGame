using SFML.System;
using TileGame.Items;
using TileGame.Tiles;

namespace TileGame.Level
{
    public class LevelTemplate
    {
        public LevelTemplate(TileAssembly tileAssembly, Vector2i mapSize, Vector2f tileSize, ItemAssembly itemAssembly)
        {
            TileAssembly = tileAssembly;
            MapSize = mapSize;
            TileSize = tileSize;
            ItemAssembly = itemAssembly;
        }

        public LevelTemplate(TileAssembly tileAssembly, Vector2i mapSize, Vector2f tileSize)
        {
            TileAssembly = tileAssembly;
            MapSize = mapSize;
            TileSize = tileSize; ;
        }

        public TileAssembly TileAssembly { get; }

        public ItemAssembly ItemAssembly { get; }

        public Vector2i MapSize { get; }
        public Vector2f TileSize { get; }
    }
}