using System;

namespace Lighthouse.Core.Scene
{
    public abstract class MainSceneKey : IEquatable<MainSceneKey>
    {
        public int Id { get; }
        public string Name { get; }

        protected MainSceneKey(int id, string name)
        {
            Id = id;
            Name = name ?? "";
        }

        public bool Equals(MainSceneKey other) => other is not null && Id == other.Id;
        public override bool Equals(object obj) => obj is MainSceneKey other && Equals(other);
        public override int GetHashCode() => Id;
        public static bool operator ==(MainSceneKey a, MainSceneKey b)
            => ReferenceEquals(a, b) || (a is not null && b is not null && a.Id == b.Id);
        public static bool operator !=(MainSceneKey a, MainSceneKey b) => !(a == b);
        public override string ToString() => string.IsNullOrEmpty(Name) ? Id.ToString() : $"{Name}({Id})";
    }
}