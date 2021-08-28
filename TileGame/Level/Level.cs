using System;
using System.Collections.Generic;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using TileGame.Character;
using TileGame.Game;
using TileGame.Interfaces;
using TileGame.Pathfinding;
using TileGame.Tiles;
using TileGame.Utility.Random;

namespace TileGame.Level
{
    public class Level : ITick, IUpdate
    {
        private readonly GameManager _gameManager;

        public delegate void LevelTask();

        public readonly Queue<LevelTask> LevelGenerationQueue = new();
        public Pathfinding.Pathfinding PathfindingVisualizer { get; set; }
        public Pathfinding.Pathfinding PathfindingWalker { get; set; }
        public Tile[,] TileMatrix { get; set; }
        public Tile SpawnTile { get; set; }
        public Tile ExitTile { get; set; }
        public List<Vector2i> EmptyTiles { get; set; }
        public Vector2i LevelSize { get; set; }

        private bool AutoPathFinding { get; set; }
        public bool FindPathOnLoad { get; set; }

        private Clock Clock { get; set; }
        private bool AutoTraverse { get; set; } = false;
        public Player ActivePlayer { get; set; }


        private delegate void UpdateHandler();

        private UpdateHandler _updateBehavior;
        public readonly PlayerMoveController PlayerMoveController;

        private int LevelQueueCreationSpeed { get; }
        public uint Identifier { get; set; } = 0;

        public Level(GameManager gameManager, Vector2i levelSize)
        {
            PlayerMoveController = new PlayerMoveController(this);
            _gameManager = gameManager;
            LevelSize = levelSize;
            LevelQueueCreationSpeed = 10;
            _updateBehavior = Tick;
            Clock = new Clock();
        }


        public Level(GameManager gameManager, Vector2i levelSize, int levelQueueCreationSpeed)
        {
            PlayerMoveController = new PlayerMoveController(this);
            _gameManager = gameManager;
            LevelSize = levelSize;
            LevelQueueCreationSpeed = levelQueueCreationSpeed;
            _updateBehavior = ProduceLevelBehavior;
            Clock = new Clock();
        }

        public void Tick()
        {
        }

        private void LevelOptionsBehavior()
        {
            if (LevelGenerationQueue.Count == 0)
            {
                ImGui.Begin("Level Options");
                {
                    if (ImGui.Button("Find Spawn to Exit path"))
                        VisualizePathfinder(SpawnTile.Node, ExitTile.Node);

                    if (ActivePlayer != null)
                    {
                        if (ImGui.Button("Toggle Pathfinding Visualization"))
                        {
                            RemoveHighlights();
                            VisualizePathfinder(ActivePlayer.OccupiedNode, ExitTile.Node);
                            AutoPathFinding = !AutoPathFinding;
                        }

                        if (ImGui.Button("Start Auto Pathfinding"))
                        {
                            RemoveHighlights();
                            AutoPathFinding = true;
                            AutoTraverse = !AutoTraverse;
                        }
                    }

                    ImGui.End();
                }
            }

            if (AutoTraverse)
            {
                AutoFindPath(ExitTile.Node);
            }
        }


        private void ProduceLevelBehavior()
        {
            if (LevelGenerationQueue.Count > 0)
                for (var i = 0; i < LevelQueueCreationSpeed; i++)
                {
                    LevelGenerationQueue.TryDequeue(out var task);
                    task?.Invoke();
                }
            else
            {
                OnLevelLoadFinished();
            }
        }

        public void GenerateRandomLevelItem(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var chance = RandomGenerator.RandomNumber(0, 2);
                var itemfactory = new ItemFactory();
                var item = chance switch
                {
                    0 => itemfactory.CreateItem("Ring"),
                    1 => itemfactory.CreateItem("Armor"),
                    2 => itemfactory.CreateItem("Weapon"),
                    _ => itemfactory.CreateItem("Ring")
                };
                ActivePlayer.ItemInventory.AddItemToFront(item);
            }
        }


        public void Update()
        {
            _updateBehavior.Invoke();
        }

        public void DestroyAllTiles()
        {
            try
            {
                if (TileMatrix == null) return;

                for (var i = 0; i < TileMatrix.GetLength(0); i++)
                for (var j = 0; j < TileMatrix.GetLength(1); j++)
                    TileMatrix[i, j].Dispose();

                _gameManager.UnloadAllGameObjects();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error at [Level.cs] - The level contents are corrupted and cannot be unloaded " +
                                  e.Message);
            }
        }

        public bool CheckTilePlaced(Vector2i tilePosition)
        {
            return TileMatrix[tilePosition.X, tilePosition.Y] == null;
        }

        public List<Vector2i> FindEmptyTiles()
        {
            EmptyTiles.Clear();
            var empty = new List<Vector2i>();
            for (var i = 0; i < TileMatrix.GetLength(0); i++)
            for (var j = 0; j < TileMatrix.GetLength(1); j++)
            {
                var tempPosition = new Vector2i(i, j);
                if (CheckTilePlaced(tempPosition)) empty.Add(tempPosition);
            }

            EmptyTiles = empty;
            return empty;
        }

        public bool TraverseCheck(Tile target)
        {
            if (target is not ITraversable) return false;
            ActivePlayer.OccupiedNode = target.Node;
            var traversable = target as ITraversable;
            traversable.OnEnter(ActivePlayer);
            target.Tick();
            if (AutoPathFinding) VisualizePathfinder(target.Node, ExitTile.Node);

            return true;
        }


        private void RemoveHighlights()
        {
            for (var i = 0; i < TileMatrix.GetLength(0); i++)
            for (var j = 0; j < TileMatrix.GetLength(1); j++)
                TileMatrix[i, j].HighlightRect.FillColor = new Color(0, 0, 0, 0);
        }

        public void CheckOccupantTile(Tile tile, Player player)
        {
            if (tile.TreasureChest != null)
                if (!tile.TreasureChest.IsUsed &&
                    player.ItemInventory.Items.Count < player.ItemInventory.MaxSlots)
                {
                    player.ItemInventory.AddItemToFront(tile.TreasureChest.Open());
                    player.Validate();
                }
        }

        private void VisualizePathfinder(Node startNode, Node endNode)
        {
            RemoveHighlights();
            PathfindingVisualizer.FindPath(startNode.MatrixPosition, endNode.MatrixPosition);
            var path = PathfindingVisualizer.Path;
            foreach (var node in path)
            {
                var tile = TileMatrix[node.MatrixPosition.X, node.MatrixPosition.Y];
                tile.HighlightRect.FillColor = new Color(195, 0, 75, 125);
            }
        }

        private void AutoFindPath(Node endNode)
        {
            // VisualizePathfinder(ActivePlayer.OccupiedNode, ExitTile.Node);
            PathfindingWalker.FindPath(ActivePlayer.OccupiedNode.MatrixPosition, endNode.MatrixPosition);

            if (!AutoTraverse || PathfindingWalker.Path.Count == 0 && PathfindingWalker.Path != null)
            {
                AutoTraverse = false;
                AutoPathFinding = false;
                return;
            }

            if (Clock.ElapsedTime.AsMilliseconds() >= 100)
            {
                if (PathfindingWalker.Path[0].MatrixPosition.X < ActivePlayer.OccupiedNode.MatrixPosition.X)
                {
                    PlayerMoveController.MovePlayerLeft();
                }

                else if (PathfindingWalker.Path[0].MatrixPosition.X > ActivePlayer.OccupiedNode.MatrixPosition.X)
                {
                    PlayerMoveController.MovePlayerRight();
                }

                else if (PathfindingWalker.Path[0].MatrixPosition.Y < ActivePlayer.OccupiedNode.MatrixPosition.Y)
                {
                    PlayerMoveController.MovePlayerUp();
                }
                else if (PathfindingWalker.Path[0].MatrixPosition.Y > ActivePlayer.OccupiedNode.MatrixPosition.Y)
                {
                    PlayerMoveController.MovePlayerDown();
                }

                PathfindingWalker.Path.Remove(PathfindingWalker.Path[0]);
                Clock.Restart();
            }
        }

        private void OnPlayerDeath()
        {
            _gameManager.GameState = GameState.Idle;
        }

        void OnLevelLoadFinished()
        {
            if (ActivePlayer != null)
            {
                ActivePlayer.PlayerDeathEvent += OnPlayerDeath;
            }

            _updateBehavior = LevelOptionsBehavior;
            if (FindPathOnLoad)
            {
                VisualizePathfinder(SpawnTile.Node, ExitTile.Node);
            }
        }
    }
}