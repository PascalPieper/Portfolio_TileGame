using System;
using TileGame.Tiles;

namespace TileGame.Game
{
    public class TileFactory : ReflectFactory<Tile>
    {
        public GameManager gameManager;

        public TileFactory(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public Tile CreateTile(string tileIdentifier)
        {
            try
            {
                var tile = GetInstance(tileIdentifier);
                gameManager.AddGameObjectToLoop(tile, tile.TileRect);
                gameManager.AddGameObjectToLoop(tile.HighlightRect);
                return tile;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR in [TileFactory.cs] - Specified Tile Name: " + tileIdentifier +
                                  " does not exist as derived type - " + e.Message);

                var tile = GetInstance(nameof(Grass));
                gameManager.AddGameObjectToLoop(tile, tile.TileRect);


                return tile;
            }
        }
    }
}