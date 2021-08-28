namespace TileGame.Interfaces
{
    public interface IMove
    {
        bool CanMove { get; }
        bool MoveUp();
        bool MoveDown();
        bool MoveLeft();
        bool MoveRight();
    }
}