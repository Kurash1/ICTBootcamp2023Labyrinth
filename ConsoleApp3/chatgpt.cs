using System;
using System.Collections.Generic;
using System.Linq;

namespace chatgpt;
class MazeGenerator
{
    static Random rng = new Random();

    public static bool[,] GenerateMaze(int width, int height)
    {
        bool[,] maze = new bool[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = true;
            }
        }

        Stack<(int, int)> stack = new Stack<(int, int)>();
        stack.Push((1, 1));
        maze[1, 1] = false;

        while (stack.Count > 0)
        {
            (int x, int y) = stack.Peek();
            List<(int, int)> neighbors = new List<(int, int)>();
            if (x > 1 && maze[x - 1, y]) neighbors.Add((x - 1, y));
            if (x < width - 2 && maze[x + 1, y]) neighbors.Add((x + 1, y));
            if (y > 1 && maze[x, y - 1]) neighbors.Add((x, y - 1));
            if (y < height - 2 && maze[x, y + 1]) neighbors.Add((x, y + 1));

            if (neighbors.Count > 0 && rng.Next(0,2) == 0)
            {
                (int nextX, int nextY) = neighbors[rng.Next(neighbors.Count)];
                maze[nextX, nextY] = false;
                stack.Push((nextX, nextY));
            }
            else
            {
                stack.Pop();
            }
        }

        // wall off the edges
        for (int x = 0; x < width; x++)
        {
            maze[x, 0] = true;
            maze[x, height - 1] = true;
        }
        for (int y = 0; y < height; y++)
        {
            maze[0, y] = true;
            maze[width - 1, y] = true;
        }
        return maze;
    }

}