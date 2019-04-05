using System;

namespace CC_Lib.Structures
{
    public class Identifiable : IEquatable<Identifiable>
    {
        public Identifiable(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public bool Equals(Identifiable other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Identifiable) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Identifiable left, Identifiable right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Identifiable left, Identifiable right)
        {
            return !Equals(left, right);
        }
    }
}
