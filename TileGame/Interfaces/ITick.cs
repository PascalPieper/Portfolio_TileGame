namespace TileGame.Interfaces
{
    public interface ITick
    {
        uint Identifier { get; }
        void Tick();
    }
}