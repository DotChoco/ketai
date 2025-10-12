namespace RDE.Structs;

public struct Vector3
{
    public int x { get; }
    public int y { get; }
    public int z { get; }

    public Vector3(int x = 0, int y = 0, int z = 0) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector3 operator +(Vector3 a, Vector3 b)
        => new (a.x + b.x, a.y + b.y, a.z + b.z);

    public static Vector3 operator -(Vector3 a, Vector3 b)
        => new (a.x - b.x, a.y - b.y, a.z - b.z);

    public static bool operator ==(Vector3 a, Vector3 b)
        => a.x == b.x && a.y == b.y && a.z == b.z;

    public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);


    // Sobreescribimos Equals y GetHashCode para coherencia con == y !=
    public override bool Equals(object obj)
    {
        if (obj is Vector3 other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

}
