using System;
using System.Collections.Generic;
using SFML.System;
using TileGame.Character;
using TileGame.Game;
using TileGame.Items;
using TileGame.Tiles;
using TileGame.Utility.Random;

namespace TileGame.Level
{
    public class LevelGenerator
    {
        private readonly GameManager _manager;

        public LevelGenerator(GameManager manager)
        {
            _manager = manager;
            TileFactory = new TileFactory(manager);
            //LevelTemplate = new LevelTemplate(tileAssembly, new Vector2i(24, 24), new Vector2f(8, 8), itemAssembly);
        }

        public LevelGenerator(GameManager manager, TileFactory tileFactory)
        {
            _manager = manager;
            TileFactory = tileFactory;
            string[] allowedTiles = { "Grass" };
            string[] allowedBlockers = { "Mountains" };
            string[] spawnableItems = { nameof(Weapon), nameof(Armor), nameof(Ring) };
            var tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);
            var itemAssembly = new ItemAssembly(spawnableItems, 0.05f, false, 0);
            LevelTemplate = new LevelTemplate(tileAssembly, new Vector2i(24, 24), new Vector2f(8, 8), itemAssembly);
        }

        public uint Identifier { get; set; } = 0;
        public LevelTemplate LevelTemplate { get; private set; }
        private TileFactory TileFactory { get; }

        public Level GenerateLevel(LevelTemplate template, int generatingSpeed, bool allowItems, bool allowPlayer)
        {
            LevelTemplate = template;
            var level = new Level(_manager, template.MapSize, generatingSpeed);

            level.TileMatrix = new Tile[template.MapSize.X, template.MapSize.Y];
            level.PathfindingVisualizer = new Pathfinding.Pathfinding(level.TileMatrix);
            level.PathfindingWalker = new Pathfinding.Pathfinding(level.TileMatrix);
            PlaceMapBarriers(template.MapSize.X, template.MapSize.Y, nameof(Mountains), level);
            PlaceEssentialTiles(template.MapSize.X, template.MapSize.Y, nameof(StartTile), level);
            GenerateRandomTiles(template, level);
            if (allowItems)
            {
                PlaceItems(level, template.ItemAssembly.SpawnFrequency);
            }

            if (allowPlayer)
            {
                SpawnPlayer(level.SpawnTile.Node.MatrixPosition.X, level.SpawnTile.Node.MatrixPosition.Y, level);
            }

            level.LevelGenerationQueue.Enqueue(() =>
            {
                if (template.ItemAssembly.SpawnPlayerWithItems && allowPlayer)
                {
                    level.GenerateRandomLevelItem(template.ItemAssembly.PlayerStartItemAmount);
                }
            });

            return level;
        }

        private Tile CreateTile(string tileName, int xPos, int yPos)
        {
            var tile = TileFactory.CreateTile(tileName);
            tile.HighlightRect.Position =
                new Vector2f(xPos * LevelTemplate.TileSize.X, yPos * LevelTemplate.TileSize.Y);
            tile.HighlightRect.Size = LevelTemplate.TileSize;
            tile.TileRect.Position = new Vector2f(xPos * LevelTemplate.TileSize.X, yPos * LevelTemplate.TileSize.Y);
            tile.TileRect.Size = LevelTemplate.TileSize;
            tile.Node.MatrixPosition = new Vector2i(xPos, yPos);
            tile.Node.WorldPosition = tile.TileRect.Position;

            return tile;
        }

        private bool GenerateChunk(Level level, string tileName, float repeatPercentage)
        {
            var result = level.FindEmptyTiles();
            var centerTile = RandomGenerator.RandomNumber(0, result.Count);
            return false;
        }

        public void SpawnPlayer(int xPos, int yPos, Level level)
        {
            level.LevelGenerationQueue.Enqueue(() =>
            {
                var itemInventory = new ItemInventory(10);
                var player = new Player(itemInventory);
                level.ActivePlayer = player;

                _manager.AddGameObjectToLoop(player, player.Sprite, player);
                player.Sprite.Position = new Vector2f(xPos * LevelTemplate.TileSize.X, yPos * LevelTemplate.TileSize.Y);
                player.OccupiedNode = level.TileMatrix[xPos, yPos].Node;
            });
        }

        public void PlaceItems(Level level, float percentage)
        {
            level.LevelGenerationQueue.Enqueue(() =>
            {
                var itemFactory = new ItemFactory(_manager);
                var unoccupiedTiles = GetUnoccupiedTiles(level);
                var itemAmount = (int)Math.Round(level.LevelSize.X * level.LevelSize.Y * percentage);

                for (var i = 0; i < itemAmount; i++)
                {
                    var rnd = RandomGenerator.RandomNumber(0, unoccupiedTiles.Count - 1);
                    var treasureChest = new TreasureChest();

                    _manager.AddGameObjectToLoop(treasureChest.Sprite);
                    treasureChest.Sprite.Position =
                        new Vector2f(unoccupiedTiles[rnd].Node.MatrixPosition.X * LevelTemplate.TileSize.X,
                            unoccupiedTiles[rnd].Node.MatrixPosition.Y * LevelTemplate.TileSize.Y);
                    level.TileMatrix[unoccupiedTiles[rnd].Node.MatrixPosition.X,
                        unoccupiedTiles[rnd].Node.MatrixPosition.Y].TreasureChest = treasureChest;

                    var itemfactory = new ItemFactory(_manager);

                    var chance = RandomGenerator.RandomNumber(0, 2);
                    var item = chance switch
                    {
                        0 => itemfactory.CreateItem("Ring"),
                        1 => itemfactory.CreateItem("Armor"),
                        2 => itemfactory.CreateItem("Weapon"),
                        _ => itemfactory.CreateItem("Ring")
                    };

                    treasureChest.HoldItem = item;
                }
            });
        }


        private List<Tile> GetUnoccupiedTiles(Level level)
        {
            var tiles = new List<Tile>();
            for (var i = 0; i < level.LevelSize.X; i++)
            for (var j = 0; j < level.LevelSize.Y; j++)
                if (level.TileMatrix[i, j] is IOccupiable)
                    tiles.Add(level.TileMatrix[i, j]);

            return tiles;
        }

        private void CreateEssentialTiles(int mapSizeX, int mapSizeY, string tileName, Level level)
        {
            string[] names = { "StartTile", "ExitTile" };
            var result = RandomGenerator.RandomNumber(0, 1);
            if (result == 1)
            {
                names[0] = nameof(ExitTile);
                names[1] = nameof(StartTile);
            }

            var number = RandomGenerator.RandomNumber(1, mapSizeY - 2);
            level.TileMatrix[1, number] = CreateTile(names[0], 1, number);
            if (level.TileMatrix[1, number] is StartTile)
            {
                level.SpawnTile = level.TileMatrix[1, number];
            }
            else
            {
                level.ExitTile = level.TileMatrix[1, number];
            }


            number = RandomGenerator.RandomNumber(1, mapSizeY - 2);
            level.TileMatrix[mapSizeX - 2, number] = CreateTile(names[1], mapSizeX - 2, number);
            if (level.TileMatrix[mapSizeX - 2, number] is ExitTile)
            {
                level.ExitTile = level.TileMatrix[mapSizeX - 2, number];
            }
            else
            {
                level.SpawnTile = level.TileMatrix[mapSizeX - 2, number];
            }
        }

        public void PlaceEssentialTiles(int mapSizeX, int mapSizeY, string tileName, Level level)
        {
            CreateEssentialTiles(mapSizeX, mapSizeY, tileName, level);
        }

        private void GenerateRandomTiles(LevelTemplate template, Level level)
        {
            for (var i = 0; i < template.MapSize.X; i++)
            for (var j = 0; j < template.MapSize.Y; j++)
            {
                var xPos = i;
                var yPos = j;
                var result = RandomGenerator.RandomNumber(0, 10);

                if (result == 0)
                    level.LevelGenerationQueue.Enqueue(() =>
                    {
                        if (level.CheckTilePlaced(new Vector2i(xPos, yPos)))
                        {
                            var tileIndex =
                                RandomGenerator.RandomNumber(0,
                                    template.TileAssembly.TraversableTiles.Length - 1);

                            level.TileMatrix[xPos, yPos] =
                                CreateTile(template.TileAssembly.TraversableTiles[tileIndex], xPos, yPos);
                        }
                    });
                else if (result == 1 || result == 2)
                    level.LevelGenerationQueue.Enqueue(() =>
                    {
                        if (level.CheckTilePlaced(new Vector2i(xPos, yPos)))
                        {
                            var tileIndex =
                                RandomGenerator.RandomNumber(0, template.TileAssembly.BlockadeTiles.Length - 1);

                            level.TileMatrix[xPos, yPos] =
                                CreateTile(template.TileAssembly.BlockadeTiles[tileIndex], xPos, yPos);
                        }
                    });
                else
                    level.LevelGenerationQueue.Enqueue(() =>
                    {
                        if (level.CheckTilePlaced(new Vector2i(xPos, yPos)))
                            level.TileMatrix[xPos, yPos] = CreateTile(template.TileAssembly.TraversableTiles[0],
                                xPos, yPos);
                    });
            }
        }

        private void PlaceMapBarriers(int mapSizeX, int mapSizeY, string tileName, Level level)
        {
            level.LevelGenerationQueue.Enqueue(() =>
            {
                for (var i = 0; i < mapSizeY; i++)
                    if (level.CheckTilePlaced(new Vector2i(0, i)))
                        level.TileMatrix[0, i] = CreateTile(tileName, 0, i);

                for (var i = 0; i < mapSizeY; i++)
                    if (level.CheckTilePlaced(new Vector2i(mapSizeX - 1, i)))
                        level.TileMatrix[mapSizeX - 1, i] = CreateTile(tileName, mapSizeX - 1, i);

                for (var i = 0; i < mapSizeX - 1; i++)
                    if (level.CheckTilePlaced(new Vector2i(i, 0)))
                        level.TileMatrix[i, 0] = CreateTile(tileName, i, 0);

                for (var i = 0; i < mapSizeX - 1; i++)
                    if (level.CheckTilePlaced(new Vector2i(i, mapSizeY - 1)))
                        level.TileMatrix[i, mapSizeY - 1] = CreateTile(tileName, i, mapSizeY - 1);
            });
        }
    }
}