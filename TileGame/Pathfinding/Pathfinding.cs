using System;
using System.Collections.Generic;
using SFML.System;
using TileGame.Tiles;

namespace TileGame.Pathfinding
{
    public class Pathfinding
    {
        public List<Node> Path;

        public Pathfinding(Tile[,] tilesMatrix)
        {
            TilesMatrix = tilesMatrix;
        }

        private Tile[,] TilesMatrix { get; }

        public void FindPath(Vector2i startPos, Vector2i targetPos)
        {
            var startNode = NodeFromWorldPoint(startPos);
            var targetNode = NodeFromWorldPoint(targetPos);

            var openSet = new List<Node>();
            var closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                var node = openSet[0];
                for (var i = 1; i < openSet.Count; i++)
                    if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
                        if (openSet[i].HCost < node.HCost)
                            node = openSet[i];

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (var neighbour in GetNeighbours(node, TilesMatrix))
                {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour)) continue;

                    var newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }

        private void RetracePath(Node startNode, Node endNode)
        {
            var path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            Path = path;
        }

        private static int GetDistance(Node nodeA, Node nodeB)
        {
            var dstX = Math.Abs(nodeA.MatrixPosition.X - nodeB.MatrixPosition.X);
            var dstY = Math.Abs(nodeA.MatrixPosition.Y - nodeB.MatrixPosition.Y);

            return dstX + dstY;
        }

        // public float NodeDiameter { get; private set; }
        // public int GridSizeX { get; private set; }
        // public int GridSizeY { get; private set; }


        public List<Node> GetNeighbours(Node node, Tile[,] tiles)
        {
            var neighbours = new List<Node>();
            var checkX = 0;
            var checkY = 0;
            for (var x = -1; x <= 1; x++)
                if (x != 0)
                {
                    checkX = node.MatrixPosition.X + x;
                    checkY = node.MatrixPosition.Y;
                    if (checkX >= 0 && checkX < tiles.GetLength(0) && checkY >= 0 && checkY < tiles.GetLength(1))
                        neighbours.Add(tiles[checkX, checkY].Node);
                }

            for (var y = -1; y <= 1; y++)
            {
                if (y == 0) continue;
                checkX = node.MatrixPosition.X + 0;
                checkY = node.MatrixPosition.Y + y;
                if (checkX >= 0 && checkX < tiles.GetLength(0) && checkY >= 0 && checkY < tiles.GetLength(1))
                    neighbours.Add(TilesMatrix[checkX, checkY].Node);
            }

            return neighbours;
        }


        private Node NodeFromWorldPoint(Vector2i startPos)
        {
            return TilesMatrix[startPos.X, startPos.Y].Node;
        }
    }
}