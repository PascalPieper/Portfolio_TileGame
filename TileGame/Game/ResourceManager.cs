using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;

namespace TileGame.Game
{
    public class ResourceManager
    {
        private static ResourceManager _instance;
        private readonly Dictionary<string, Font> _fonts = new();
        private readonly Dictionary<string, Sound> _sounds = new();
        private readonly Dictionary<string, Texture> _textures = new();
        private string ExecuteableDirectoryPath;

        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ResourceManager();
                return _instance;
            }
        }

        public Texture LoadTexture(string path)
        {
            var fullPath = AppDomain.CurrentDomain.BaseDirectory + path;

            if (_textures.ContainsKey(path)) return _textures[path];

            var texture = new Texture(fullPath);

            _textures.Add(path, texture);
            return texture;
        }

        public Texture GetTexture(string name)
        {
            return _textures[name];
        }


        public bool LoadSoundFromFile(string name, string path)
        {
            var soundBuffer = new SoundBuffer(path);
            var s = new Sound(soundBuffer);
            _sounds.Add(name, s);

            return true;
        }

        public bool LoadFontFromFile(string name, string path)
        {
            var font = new Font(path);
            _fonts.Add(name, font);

            return true;
        }


        public Sound GetSound(string name)
        {
            return _sounds[name];
        }

        public Font GetFont(string name)
        {
            return _fonts[name];
        }
    }
}