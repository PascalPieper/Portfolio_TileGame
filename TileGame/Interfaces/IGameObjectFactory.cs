using System;
using SFML.Graphics;
using SFML.System;
using TileGame.Interfaces;

namespace TileGame.Game
{
    public interface IGameObjectFactory
    {
        event EventHandler<ITick> OnTickEntityCreation;
        event EventHandler<Drawable> OnDrawableEntityCreation;
        T CreateEntity<T>(Vector2f spawnPosition) where T : Transformable, ITick, Drawable, new();
    }
}