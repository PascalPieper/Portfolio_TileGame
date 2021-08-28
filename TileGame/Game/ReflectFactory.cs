using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace TileGame.Game
{
    public abstract class ReflectFactory<T>
    {
        private static readonly ImmutableDictionary<string, Type> TypesByName;

        static ReflectFactory()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, Type>();
            var tileTypes = Assembly.GetAssembly(typeof(T))?.GetTypes()
                .Where(myType => !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));

            foreach (var type in tileTypes) builder.Add(type.Name, type);
            TypesByName = builder.ToImmutable();
        }


        public virtual T GetInstance(string typeName)
        {
            if (!TypesByName.ContainsKey(typeName))
                throw new Exception($"{this} does not contain the name of {typeName}");
            var type = TypesByName[typeName];
            var instance = Activator.CreateInstance(type);
            return (T)instance;
        }
    }
}