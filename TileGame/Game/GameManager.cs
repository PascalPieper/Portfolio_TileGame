using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using TileGame.Interfaces;
using TileGame.Tiles;

namespace TileGame.Game
{
    public class GameManager

    {
        private readonly List<Drawable> _drawableGameObjects;
        private readonly Dictionary<uint, ITick> _tickingGameObjects;
        private readonly Dictionary<uint, IUpdate> _updatingGameObjects;
        public GameState GameState = GameState.Idle;

        public GameManager()
        {
            _tickingGameObjects = new Dictionary<uint, ITick>();
            _drawableGameObjects = new List<Drawable>();
            _updatingGameObjects = new Dictionary<uint, IUpdate>();
        }

        private uint IdCount { get; set; }

        public void Tick()
        {
            for (var index = _tickingGameObjects.Count; index > 0; index--)
            {
                var item = _tickingGameObjects.ElementAt(index);
                item.Value.Tick();
            }
        }

        public void Draw(RenderWindow window)
        {
            List<Drawable> orderedList;


            foreach (var entity in _drawableGameObjects) window.Draw(entity);
        }

        public void Update()
        {
            for (var index = _updatingGameObjects.Count; index > 0; index--)
            {
                var item = _updatingGameObjects.ElementAt(index - 1);
                item.Value.Update();
            }
        }

        public void AddGameObjectToLoop(ITick tickingGo, Drawable drawableGo)
        {
            _tickingGameObjects.Add(IdCount, tickingGo);
            _drawableGameObjects.Add(drawableGo);
            IdCount++;
        }

        public void AddGameObjectToLoop(Drawable drawableGo)
        {
            _drawableGameObjects.Add(drawableGo);
            IdCount++;
        }

        public void AddGameObjectToLoop(ITick tickingGo, Drawable drawableGo, IUpdate updateableGo)
        {
            _tickingGameObjects.Add(IdCount, tickingGo);
            _drawableGameObjects.Add(drawableGo);

            _updatingGameObjects.Add(IdCount, updateableGo);
            IdCount++;
        }

        public void UnloadAllGameObjects()
        {
            foreach (var entry in _tickingGameObjects)
            {
                _tickingGameObjects.Remove(entry.Key);
            }

            _drawableGameObjects.Clear();

            foreach (var entry in _updatingGameObjects) _updatingGameObjects.Remove(entry.Key);

            IdCount = 0;
        }
    }
}