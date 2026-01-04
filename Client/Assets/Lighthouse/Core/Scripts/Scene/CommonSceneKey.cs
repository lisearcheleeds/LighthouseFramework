using System;

namespace Lighthouse.Core.Scene
{
    public abstract class CommonSceneKey : IEquatable<CommonSceneKey>
    {
        public int Id { get; }
        public string Name { get; }

        protected CommonSceneKey(int id, string name)
        {
            Id = id;
            Name = name ?? "";
        }

        public bool Equals(CommonSceneKey other) => other is not null && Id == other.Id;
        public override bool Equals(object obj) => obj is CommonSceneKey other && Equals(other);
        public override int GetHashCode() => Id;
        public static bool operator ==(CommonSceneKey a, CommonSceneKey b)
            => ReferenceEquals(a, b) || (a is not null && b is not null && a.Id == b.Id);
        public static bool operator !=(CommonSceneKey a, CommonSceneKey b) => !(a == b);
        public override string ToString() => string.IsNullOrEmpty(Name) ? Id.ToString() : $"{Name}({Id})";
    }
}