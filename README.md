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
