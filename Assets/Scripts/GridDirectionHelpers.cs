public static class GridDirectionHelpers
{
    public static GridDirection[] AllDirections = { GridDirection.N, GridDirection.NE, GridDirection.E, GridDirection.SE, GridDirection.S, GridDirection.SW, GridDirection.W, GridDirection.NW };
    public static GridDirection[] Horizontal = { GridDirection.E, GridDirection.W };
    public static GridDirection[] Vertical = { GridDirection.N, GridDirection.S };
    public static GridDirection[] StraightDirections = { GridDirection.N, GridDirection.E, GridDirection.S, GridDirection.W };
    public static GridDirection[] DiagonalDirections = { GridDirection.NE, GridDirection.SE, GridDirection.SW, GridDirection.NW };
}