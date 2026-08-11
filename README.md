# A* Pathfinding Algorithm

A C# implementation of the **A\ * (A-Star) search algorithm** — one of the most widely used pathfinding and graph traversal techniques in computer science and game development.

## Overview

A* finds the shortest path between a start node and a goal node by combining the benefits of Dijkstra's algorithm (cost from start) and a greedy best-first search (estimated cost to goal). The heuristic function guides the search, making it both complete and optimal under standard conditions.

## Features

- Full A* search implementation in C#
- Configurable heuristic function (Manhattan, Euclidean)
- Grid-based pathfinding with obstacle support
- Visual path output for debugging and demonstration

## Tech Stack

| | |
|---|---|
| Language | C# |
| Platform | .NET |
| Paradigm | Object-Oriented Programming |

## How It Works

1. Initialize the open list with the start node
2. Evaluate neighbors using `f(n) = g(n) + h(n)` where:
   - `g(n)` = cost from start to current node
   - `h(n)` = heuristic estimate from current node to goal
3. Expand the lowest-cost node until the goal is reached
4. Reconstruct the optimal path from goal back to start

## Academic Context

Built as a graduate Computer Science project at **Saint Martin's University**, exploring heuristic search algorithms, graph theory, and algorithmic efficiency.

---

**Developer:** Landon Armstrong | [GitHub](https://github.com/Larmstrong1127) | [LinkedIn](https://linkedin.com/in/landon-armstrong)

---

## Unit Tests

An xUnit test project lives in the `AStarTests/` directory. It validates correctness and performance of the `FindPath()` method — a testable companion to `AStar()` that returns a `List<(int row, int col)>?` instead of printing to the console.

### How to Run

```bash
cd AStarTests && dotnet test
```

### Test Cases

| Test | Description |
|---|---|
| `FindPath_StraightLine_ReturnsPath` | Finds a path across a 1×5 open grid from (0,0) to (0,4) |
| `FindPath_AlreadyAtDestination_ReturnsSingleCell` | When src == dest, returns a single-element list |
| `FindPath_NoPathExists_ReturnsNull` | Returns null when a wall of blocked cells separates src and dest |
| `FindPath_MazeNavigation_FindsValidPath` | Runs the exact 9×10 grid from Program.cs (src=(8,0), dest=(0,0)) and validates every step is adjacent |
| `FindPath_InvalidSource_ReturnsNull` | Returns null when source is out of bounds (row=-1) |
| `FindPath_InvalidDestination_ReturnsNull` | Returns null when destination is out of bounds |
| `FindPath_BlockedSource_ReturnsNull` | Returns null when the source cell is blocked (value = 0) |
| `IsValid_BoundaryConditions_CorrectResults` | Checks IsValid() at (0,0), (-1,0), (5,0), and (4,4) on a 5×5 grid |
| `CalculateHValue_EuclideanDistance_IsAccurate` | Verifies the heuristic gives sqrt(3²+4²)=5.0 from (0,0) to (3,4) |
| `FindPath_PerformanceBenchmark_CompletesInTime` | Times FindPath on a 50×50 all-open grid; asserts completion in under 1000 ms |

### Performance Note

The `FindPath_PerformanceBenchmark_CompletesInTime` test demonstrates that the A* implementation scales efficiently. The assertion is the honest bound: the test fails if a 50×50 fully open grid (2,500 cells) takes longer than 1000 ms. It runs far under that in practice, but the committed threshold is the only number here backed by an artifact, so it is the only one quoted.
