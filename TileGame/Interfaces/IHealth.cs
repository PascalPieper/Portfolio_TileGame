namespace TileGame.Character
{
    public interface IHealth
    {
        int Health { get; }
        void TakeDamage(int amount);
    }
}