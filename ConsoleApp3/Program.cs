using Pastel;
using System.Numerics;
using chatgpt;

const bool none = false;
bool wall = true;
bool[,] dungeon = new bool[,] {
     { wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall },
     { wall, none, none, none, none, none, none, none, none, none, wall, none, none, none, wall },
     { wall, none, none, none, wall, none, wall, wall, wall, none, wall, none, wall, none, wall },
     { wall, none, none, none, wall, none, wall, none, none, none, wall, none, wall, none, wall },
     { wall, none, none, wall, wall, wall, wall, none, wall, wall, wall, wall, wall, none, wall },
     { wall, none, wall, wall, none, none, wall, none, none, none, none, none, none, none, wall },
     { wall, none, wall, none, none, wall, wall, wall, wall, wall, wall, wall, wall, none, wall },
     { wall, none, wall, wall, none, none, none, wall, none, none, none, none, none, none, wall },
     { wall, none, none, none, none, wall, none, wall, none, wall, wall, wall, wall, wall, wall },
     { wall, wall, wall, wall, wall, wall, none, wall, none, none, none, none, none, none, wall },
     { wall, none, none, none, none, wall, none, wall, wall, wall, wall, none, wall, none, wall },
     { wall, none, wall, wall, wall, wall, none, wall, none, none, wall, none, wall, none, wall },
     { wall, none, none, none, none, none, none, wall, none, none, wall, none, wall, none, wall },
     { wall, none, wall, wall, none, none, none, wall, none, wall, wall, wall, wall, none, wall },
     { wall, none, none, none, none, none, none, none, none, none, none, none, wall, none, wall },
     { wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall, wall }
};

int height = 16;
int width = 15;
//bool[,] dungeon = new bool[width, height];
//dungeon = MazeGenerator.GenerateMaze(width,height);

string box   = "██";
string empty = "  ";

for(int y = 0; y < height; y++)
{
    for(int x = 0; x < width; x++)
    {
        Console.Write(dungeon[y,x]?box:empty);
    }
    Console.WriteLine();
}
List<point> path = new List<point>();
point botPos = new point(1,1);
point targetpos = new point(13, 14);
Console.WriteLine(Distance(botPos, targetpos));
Console.SetCursorPosition(botPos.X * 2, botPos.Y);
Console.Write(box.Pastel(ConsoleColor.Red));
Console.SetCursorPosition(targetpos.X * 2, targetpos.Y);
Console.Write(box.Pastel(ConsoleColor.Red));
while (!botPos.isEqual(targetpos)) {
    Console.ReadKey();
    doCheck();
}
for(int i = 0; i < path.Count; i++)
{
    Console.SetCursorPosition(path[i].X*2, path[i].Y);
    Console.WriteLine(box.Pastel(ConsoleColor.Magenta));
}
void doCheck()
{
    point check = CheckAround();
    if (check.isEqual(botPos))
    {
        while (NoAdjacentFree())
        {
            path.Remove(path.Last());
            botPos = path.Last();
            check = CheckAround();
        }
    }
    path.Add(check);
    Console.SetCursorPosition(check.X * 2, check.Y);
    Console.Write(box.Pastel(ConsoleColor.Green));
    dungeon[check.Y, check.X] = wall;
    botPos = check;
}
bool NoAdjacentFree()
{
    for (int x = -1; x < 2; x++)
    {
        for (int y = -1; y < 2; y++)
        {
            if (x == 0 && y == 0)
                continue;
            if (!dungeon[botPos.Y + y, botPos.X + x])
                return false;
        }
    }
    return true;
}
point CheckAround()
{
    float LowestDistance = float.PositiveInfinity;
    point point = new(botPos.X, botPos.Y);
    for(int x = -1; x < 2; x++)
    {
        for(int y = -1; y < 2; y++) 
        {
            if (x == 0 && y == 0)
                continue;
            if (dungeon[botPos.Y + y, botPos.X + x])
                continue;
            if(check(x,y) < LowestDistance)
            {
                LowestDistance = check(x, y);
                point = new point(botPos.X + x, botPos.Y + y);
            }
        }
    }
    return point;
    float check(int x, int y)
    {
        return Distance(new point(botPos.X + x, botPos.Y + y), targetpos);
    }
}
float Distance(point a, point b)
{
    return MathF.Sqrt(MathF.Pow(a.X - b.X, 2) + MathF.Pow(a.Y - b.Y, 2));
}
public class point
{
    public int X;
    public int Y;
    public point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public bool isEqual(point other)
    {
        return this.X == other.X && this.Y == other.Y;
    }
}