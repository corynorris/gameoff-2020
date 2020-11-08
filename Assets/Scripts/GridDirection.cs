
public enum GridDirection
{
    N, NE, E, SE, S, SW, W, NW
}

public static class GridDirectionExtensions
{
    public static GridDirection Opposite(this GridDirection direction)
    {
        return (int)direction < 4 ? (direction + 4) : (direction - 4);
    }

    public static GridDirection Previous(this GridDirection direction)
    {
        return direction == GridDirection.N ? GridDirection.NW : (direction - 1);
    }

    public static GridDirection Next(this GridDirection direction)
    {
        return direction == GridDirection.NW ? GridDirection.N : (direction + 1);
    }

}

