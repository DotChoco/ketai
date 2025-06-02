namespace RDE.Structs;

public struct Vector4 {
    public int x { get; }
    public int y { get; }
    public int z { get; }
    public int w { get; }

    public Vector4(int x = 0, int y = 0, int z = 0, int w = 0) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    
    public static Vector4 operator +(Vector4 a, Vector4 b)
        => new (a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    
    public static Vector4 operator -(Vector4 a, Vector4 b)
        => new (a.x - b.x, a.y - b.y, a.z - b.z);
    
    public static bool operator ==(Vector4 a, Vector4 b) 
        => a.x == b.x && a.y == b.y && a.z == b.z;

    public static bool operator !=(Vector4 a, Vector4 b) => !(a == b);
    
    
    // Sobreescribimos Equals y GetHashCode para coherencia con == y !=
    public override bool Equals(object obj)
    {
        if (obj is Vector4 other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

}