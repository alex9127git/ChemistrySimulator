public class Coordinate
{
    private int x;
    private int y;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }

    public override bool Equals(object obj)
    {
        return obj is Coordinate && x == ((Coordinate)obj).x && y == ((Coordinate)obj).y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
