using System;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using TileGame.Game;
using TileGame.Items;
using TileGame.Storage;
using TileGame.Tiles;
using GuiImpl = TileGame.Utility.SfmlExtension.GuiImpl;

namespace TileGame.Main
{
    internal class GameWindow
    {
        private View _activeview;
        private int _generationSpeed = 15;
        private int _mapsizeX = 32;
        private int _mapsizeY = 32;
        private float _itemSpawnFrequency = 0.025f;
        private Storage.Level _activeLevel;
        private Time _deltaTime;
        private readonly Clock _deltaTimeClock;

        public GameWindow()
        {
            _deltaTimeClock = new Clock();
            _deltaTime = new Time();
        }

        public void Run()
        {
            //Window Settings
            var mode = new VideoMode(1920, 1080);
            var view1 = new View(new FloatRect(-120, -15, 512, 288));
            _activeview = view1;
            //view1.Zoom(2);
            var window = new RenderWindow(mode, "TileGame Portfolio");
            window.Closed += new EventHandler(Window_close_event);
            window.SetFramerateLimit(60);
            window.SetKeyRepeatEnabled(true);

            //Setup for Input
            window.KeyPressed += Window_KeyPressed;
            window.MouseWheelScrolled += Window_MousePressed;
            GuiImpl.Init(window);
            ImGui.LoadIniSettingsFromDisk("imgui.ini");

            //Instantiation of crucial Managers
            var gameManager = new GameManager();

            // Start the game loop
            while (window.IsOpen)
            {
                _deltaTime = _deltaTimeClock.Restart();
                GuiImpl.Update(window, _deltaTime);

                #region ImGui Interface
                
                
                if (ImGui.Begin("Level Selection"))
                {
                    if (ImGui.SliderInt("Spawn Speed", ref _generationSpeed, 1, 35))
                    {
                    }

                    ImGui.SliderInt("Map Size X", ref _mapsizeX, 6, 50);
                    ImGui.SliderInt("Map Size Y", ref _mapsizeY, 6, 50);

                    ImGui.SliderFloat("Item Spawn Rate", ref _itemSpawnFrequency, 0, 0.4f);

                    if (ImGui.Button("Load Exercise 1a & 1b"))
                    {
                        UnloadLevel(gameManager, ref _activeLevel);
                        string[] allowedTiles = { nameof(Grass) };
                        string[] allowedBlockers = { nameof(Mountains) };

                        var tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);

                        string[] spawnableItems = { nameof(Weapon), nameof(Armor), nameof(Ring) };
                        var itemAssembly = new ItemAssembly(spawnableItems, _itemSpawnFrequency, false, 0);

                        var generator = new LevelGenerator(gameManager);
                        var template = new LevelTemplate(tileAssembly, new Vector2i(_mapsizeX, _mapsizeY),
                            new Vector2f(8, 8), itemAssembly);

                        _activeLevel = generator.GenerateLevel(template, _generationSpeed, false, false);
                    }

                    if (ImGui.Button("Load Exercise 1c"))
                    {
                        UnloadLevel(gameManager, ref _activeLevel);
                        string[] allowedTiles = { nameof(Grass), nameof(PoisonSwamp) };
                        string[] allowedBlockers = { nameof(Mountains) };

                        var tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);

                        string[] spawnableItems = { nameof(Weapon), nameof(Armor), nameof(Ring) };
                        var itemAssembly = new ItemAssembly(spawnableItems, _itemSpawnFrequency, false, 0);

                        var generator = new LevelGenerator(gameManager);
                        var template = new LevelTemplate(tileAssembly, new Vector2i(_mapsizeX, _mapsizeY),
                            new Vector2f(8, 8), itemAssembly);

                        _activeLevel = generator.GenerateLevel(template, _generationSpeed, true, false);
                    }


                    if (ImGui.Button("Load Exercise 2a & 2b"))
                    {
                        UnloadLevel(gameManager, ref _activeLevel);
                        string[] allowedTiles = { nameof(Grass), nameof(PoisonSwamp) };
                        string[] allowedBlockers = { nameof(Mountains) };

                        var tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);

                        string[] spawnableItems = { nameof(Weapon), nameof(Armor), nameof(Ring) };
                        var itemAssembly = new ItemAssembly(spawnableItems, _itemSpawnFrequency, false, 0);

                        var generator = new LevelGenerator(gameManager);
                        var template = new LevelTemplate(tileAssembly, new Vector2i(_mapsizeX, _mapsizeY),
                            new Vector2f(8, 8), itemAssembly);

                        _activeLevel = generator.GenerateLevel(template, _generationSpeed, true, true);
                    }

                    if (ImGui.Button("Load Exercise 3"))
                    {
                        UnloadLevel(gameManager, ref _activeLevel);
                        string[] allowedTiles = { nameof(Grass), nameof(PoisonSwamp) };
                        string[] allowedBlockers = { nameof(Mountains) };

                        var tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);

                        string[] spawnableItems = { nameof(Weapon), nameof(Armor), nameof(Ring) };
                        var itemAssembly = new ItemAssembly(spawnableItems, _itemSpawnFrequency, true, 10);

                        var generator = new LevelGenerator(gameManager);
                        var template = new LevelTemplate(tileAssembly, new Vector2i(_mapsizeX, _mapsizeY),
                            new Vector2f(8, 8), itemAssembly);

                        _activeLevel = generator.GenerateLevel(template, _generationSpeed, true, true);
                    }

                    if (ImGui.Button("Load Exercise 4a"))
                    {
                        UnloadLevel(gameManager, ref _activeLevel);
                        string[] allowedTiles = { nameof(Grass), nameof(PoisonSwamp) };
                        string[] allowedBlockers = { nameof(Mountains) };

                        var tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);

                        string[] spawnableItems = { nameof(Weapon), nameof(Armor), nameof(Ring) };
                        var itemAssembly = new ItemAssembly(spawnableItems, _itemSpawnFrequency, true, 10);

                        var generator = new LevelGenerator(gameManager);
                        var template = new LevelTemplate(tileAssembly, new Vector2i(_mapsizeX, _mapsizeY),
                            new Vector2f(8, 8), itemAssembly);

                        _activeLevel = generator.GenerateLevel(template, _generationSpeed, false, false);
                        _activeLevel.FindPathOnLoad = true;
                    }

                    if (ImGui.Button("Load Exercise 5a"))
                    {
                        UnloadLevel(gameManager, ref _activeLevel);
                        string[] allowedTiles = { nameof(Grass), nameof(PoisonSwamp) };
                        string[] allowedBlockers = { nameof(Mountains) };

                        var tileAssembly = new TileAssembly(allowedTiles, allowedBlockers);

                        string[] spawnableItems = { nameof(Weapon), nameof(Armor), nameof(Ring) };
                        var itemAssembly = new ItemAssembly(spawnableItems, _itemSpawnFrequency, false, 0);

                        var generator = new LevelGenerator(gameManager);
                        var template = new LevelTemplate(tileAssembly, new Vector2i(_mapsizeX, _mapsizeY),
                            new Vector2f(8, 8), itemAssembly);

                        _activeLevel = generator.GenerateLevel(template, _generationSpeed, true, true);
                    }

                    if (ImGui.Button("Unload Current Level")) UnloadLevel(gameManager, ref _activeLevel);

                    if (ImGui.Button("Close Game"))
                    {
                        ImGui.SaveIniSettingsToDisk("imgui.ini");
                        window.Close();
                    }

                    ImGui.End();
                }

                if (ImGui.Begin("Camera"))
                {
                    ImGui.Columns(2);
                    if (ImGui.Button("Increase Zoom")) view1.Zoom(0.95f);

                    if (ImGui.Button("Decrease Zoom")) view1.Zoom(1.05f);

                    ImGui.NextColumn();
                    if (ImGui.Button("Move Camera Right"))
                        view1.Center = new Vector2f(view1.Center.X - 25, view1.Center.Y);

                    if (ImGui.Button("Move Camera Left"))
                        view1.Center = new Vector2f(view1.Center.X + 25, view1.Center.Y);

                    if (ImGui.Button("Move Camera Up")) view1.Center = new Vector2f(view1.Center.X, view1.Center.Y - 5);

                    if (ImGui.Button("Move Camera Down"))
                        view1.Center = new Vector2f(view1.Center.X, view1.Center.Y + 5);

                    ImGui.End();
                }

                #endregion

                _activeLevel?.Update();
                gameManager.Update();
                Notifier.Display();

                // Process events
                window.SetView(view1);
                window.DispatchEvents();
                gameManager.Draw(window);
                GuiImpl.Render();
                window.Display();
                window.Clear();

                _deltaTimeClock.Restart();
            }
        }

        private void Window_close_event(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Close();
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            var window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape) window.Close();

            if (_activeLevel != null && _activeLevel.LevelGenerationQueue.Count == 0 &&
                _activeLevel.ActivePlayer != null)
            {
                if (e.Code is Keyboard.Key.Left or Keyboard.Key.A)
                {
                    _activeLevel.PlayerMoveController.MovePlayerLeft();
                }

                if (e.Code is Keyboard.Key.Right or Keyboard.Key.D)
                {
                    _activeLevel.PlayerMoveController.MovePlayerRight();
                }

                if (e.Code is Keyboard.Key.Up or Keyboard.Key.W)
                {
                    _activeLevel.PlayerMoveController.MovePlayerUp();
                }

                if (e.Code is Keyboard.Key.Down or Keyboard.Key.S)
                {
                    _activeLevel.PlayerMoveController.MovePlayerDown();
                }
            }
        }

        private void Window_MousePressed(object sender, MouseWheelScrollEventArgs s)
        {
            var window = (Window)sender;
            if (s.Wheel == Mouse.Wheel.VerticalWheel)
            {
                if (s.Delta > 0)
                    _activeview.Zoom(0.93f);
                else
                    _activeview.Zoom(1.08f);
            }
        }

        private void UnloadLevel(GameManager gm, ref Storage.Level activeLevel)
        {
            gm?.UnloadAllGameObjects();
            activeLevel?.DestroyAllTiles();
        }
    }
}