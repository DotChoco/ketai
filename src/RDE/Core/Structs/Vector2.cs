namespace RDE.Core.Structs;
public struct Vector2 {
    public int x { get; }
    public int y { get; }

    public Vector2(int x = 0, int y = 0) {
        this.x = x;
        this.y = y;
    }

    public static Vector2 operator +(Vector2 a, Vector2 b)
        => new (a.x + b.x, a.y + b.y);

    public static Vector2 operator -(Vector2 a, Vector2 b)
    => new (a.x - b.x, a.y - b.y);

    public static Vector2 operator *(Vector2 v, int scalar)
        => new (v.x * scalar, v.y * scalar);

    public static bool operator ==(Vector2 a, Vector2 b)
        => a.x == b.x && a.y == b.y;

    public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);


    // Sobreescribimos Equals y GetHashCode para coherencia con == y !=
    public override bool Equals(object obj){
      if (obj is Vector2 other)
        return this == other;
      return false;
    }

    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
}
