using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

public class AStarSearchTests
{
    // 9x10 grid from Program.cs
    private static int[,] ProgramGrid => new int[,]
    {
        {1, 0, 1, 1, 1, 1, 0, 1, 1, 1},
        {1, 1, 1, 0, 1, 1, 1, 0, 1, 1},
        {1, 1, 1, 0, 1, 1, 0, 1, 0, 1},
        {0, 0, 1, 0, 1, 0, 0, 0, 0, 1},
        {1, 1, 1, 0, 1, 1, 1, 0, 1, 0},
        {1, 0, 1, 1, 1, 1, 0, 1, 0, 0},
        {1, 0, 0, 0, 0, 1, 0, 0, 0, 1},
        {1, 0, 1, 1, 1, 1, 0, 1, 1, 1},
        {1, 1, 1, 0, 0, 0, 1, 0, 0, 1}
    };

    [Fact]
    public void FindPath_StraightLine_ReturnsPath()
    {
        // 1x5 open grid, src=(0,0) dest=(0,4)
        int[,] grid = { { 1, 1, 1, 1, 1 } };
        var src = new AStarSearch.Pair(0, 0);
        var dest = new AStarSearch.Pair(0, 4);

        var path = AStarSearch.FindPath(grid, src, dest);

        Assert.NotNull(path);
        Assert.Equal((0, 0), path![0]);
        Assert.Equal((0, 4), path[path.Count - 1]);
    }

    [Fact]
    public void FindPath_AlreadyAtDestination_ReturnsSingleCell()
    {
        int[,] grid = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
        var src = new AStarSearch.Pair(1, 1);
        var dest = new AStarSearch.Pair(1, 1);

        var path = AStarSearch.FindPath(grid, src, dest);

        Assert.NotNull(path);
        var single = Assert.Single(path!);
        Assert.Equal((src.first, src.second), single);
    }

    [Fact]
    public void FindPath_NoPathExists_ReturnsNull()
    {
        // 3x3 grid with a wall of 0s separating src from dest
        int[,] grid =
        {
            { 1, 0, 1 },
            { 1, 0, 1 },
            { 1, 0, 1 }
        };
        var src = new AStarSearch.Pair(0, 0);
        var dest = new AStarSearch.Pair(0, 2);

        var result = AStarSearch.FindPath(grid, src, dest);

        Assert.Null(result);
    }

    [Fact]
    public void FindPath_MazeNavigation_FindsValidPath()
    {
        // Use the exact 9x10 grid from Program.cs, src=(8,0) dest=(0,0)
        var src = new AStarSearch.Pair(8, 0);
        var dest = new AStarSearch.Pair(0, 0);

        var path = AStarSearch.FindPath(ProgramGrid, src, dest);

        Assert.NotNull(path);
        Assert.Equal((8, 0), path![0]);
        Assert.Equal((0, 0), path[path.Count - 1]);

        // Every cell in path must be adjacent (diagonal allowed) to the next
        for (int i = 0; i < path.Count - 1; i++)
        {
            var (r1, c1) = path[i];
            var (r2, c2) = path[i + 1];
            int rowDiff = Math.Abs(r2 - r1);
            int colDiff = Math.Abs(c2 - c1);
            Assert.True(rowDiff <= 1 && colDiff <= 1,
                $"Step {i}: ({r1},{c1}) to ({r2},{c2}) is not adjacent");
        }
    }

    [Fact]
    public void FindPath_InvalidSource_ReturnsNull()
    {
        int[,] grid = { { 1, 1 }, { 1, 1 } };
        var src = new AStarSearch.Pair(-1, 0);
        var dest = new AStarSearch.Pair(1, 1);

        var result = AStarSearch.FindPath(grid, src, dest);

        Assert.Null(result);
    }

    [Fact]
    public void FindPath_InvalidDestination_ReturnsNull()
    {
        int[,] grid = { { 1, 1 }, { 1, 1 } };
        var src = new AStarSearch.Pair(0, 0);
        var dest = new AStarSearch.Pair(5, 5);

        var result = AStarSearch.FindPath(grid, src, dest);

        Assert.Null(result);
    }

    [Fact]
    public void FindPath_BlockedSource_ReturnsNull()
    {
        int[,] grid = { { 0, 1 }, { 1, 1 } };
        var src = new AStarSearch.Pair(0, 0); // cell is 0 (blocked)
        var dest = new AStarSearch.Pair(1, 1);

        var result = AStarSearch.FindPath(grid, src, dest);

        Assert.Null(result);
    }

    [Fact]
    public void IsValid_BoundaryConditions_CorrectResults()
    {
        Assert.True(AStarSearch.IsValid(0, 0, 5, 5));
        Assert.False(AStarSearch.IsValid(-1, 0, 5, 5));
        Assert.False(AStarSearch.IsValid(5, 0, 5, 5));
        Assert.True(AStarSearch.IsValid(4, 4, 5, 5));
    }

    [Fact]
    public void CalculateHValue_EuclideanDistance_IsAccurate()
    {
        // From (0,0) to (3,4): sqrt(3^2 + 4^2) = sqrt(9+16) = sqrt(25) = 5.0
        var dest = new AStarSearch.Pair(3, 4);
        double result = AStarSearch.CalculateHValue(0, 0, dest);
        Assert.True(Math.Abs(result - 5.0) < 0.001,
            $"Expected ~5.0 but got {result}");
    }

    [Fact]
    public void FindPath_PerformanceBenchmark_CompletesInTime()
    {
        // 50x50 all-open grid (all 1s)
        int size = 50;
        int[,] grid = new int[size, size];
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                grid[i, j] = 1;

        var src = new AStarSearch.Pair(0, 0);
        var dest = new AStarSearch.Pair(size - 1, size - 1);

        var sw = Stopwatch.StartNew();
        var path = AStarSearch.FindPath(grid, src, dest);
        sw.Stop();

        Console.WriteLine($"FindPath on 50x50 grid completed in {sw.ElapsedMilliseconds} ms");

        Assert.NotNull(path);
        Assert.True(sw.ElapsedMilliseconds < 1000,
            $"Expected completion in <1000ms but took {sw.ElapsedMilliseconds}ms");
    }
}
