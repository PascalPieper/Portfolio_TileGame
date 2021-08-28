using TileGame.Character;

namespace TileGame.Interfaces
{
    public interface ITraversable
    {
        void OnEnter(Player player);
        void OnExit();
    }
}