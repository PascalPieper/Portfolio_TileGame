using TileGame.Character;

namespace TileGame.Level
{
    public class PlayerMoveController
    {
        private Level _level;

        public PlayerMoveController(Level level)
        {
            _level = level;
        }

        public void MovePlayerRight()
        {
            if (_level.ActivePlayer.CanMove)
            {
                var target = _level.TileMatrix[_level.ActivePlayer.OccupiedNode.MatrixPosition.X + 1,
                    _level.ActivePlayer.OccupiedNode.MatrixPosition.Y];
                if (_level.TraverseCheck(target))
                {
                    _level.ActivePlayer.MoveRight();
                    _level.CheckOccupantTile(target, _level.ActivePlayer);
                }
            }
        }

        public void MovePlayerLeft()
        {
            if (_level.ActivePlayer.CanMove)
            {
                var target = _level.TileMatrix[_level.ActivePlayer.OccupiedNode.MatrixPosition.X - 1,
                    _level.ActivePlayer.OccupiedNode.MatrixPosition.Y];
                if (_level.TraverseCheck(target))
                {
                    _level.ActivePlayer.MoveLeft();
                    _level.CheckOccupantTile(target, _level.ActivePlayer);
                }
            }
        }

        public void MovePlayerUp()
        {
            if (_level.ActivePlayer.CanMove)
            {
                var target = _level.TileMatrix[_level.ActivePlayer.OccupiedNode.MatrixPosition.X,
                    _level.ActivePlayer.OccupiedNode.MatrixPosition.Y - 1];
                if (_level.TraverseCheck(target))
                {
                    _level.ActivePlayer.MoveUp();
                    _level.CheckOccupantTile(target, _level.ActivePlayer);
                }
            }
        }

        public void MovePlayerDown()
        {
            if (_level.ActivePlayer.CanMove)
            {
                var target = _level.TileMatrix[_level.ActivePlayer.OccupiedNode.MatrixPosition.X,
                    _level.ActivePlayer.OccupiedNode.MatrixPosition.Y + 1];
                if (_level.TraverseCheck(target))
                {
                    _level.ActivePlayer.MoveDown();
                    _level.CheckOccupantTile(target, _level.ActivePlayer);
                }
            }
        }
    }
}