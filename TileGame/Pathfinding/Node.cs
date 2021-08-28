using SFML.System;

namespace TileGame.Pathfinding
{
    public class Node
    {
        public int GCost;
        public int HCost;

        public Node Parent;


        public Node(bool walkable, Vector2f worldPos, Vector2i matrixPosition)
        {
            Walkable = walkable;
            WorldPosition = worldPos;
            MatrixPosition = matrixPosition;
        }

        public bool Walkable { get; set; }
        public Vector2f WorldPosition { get; set; }

        public Vector2i MatrixPosition { get; set; }

        public int FCost => GCost + HCost;
    }
}