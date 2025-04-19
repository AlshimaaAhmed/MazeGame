using UnityEngine;
using System;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public int width = 21, height = 21;
    public Material brick;

    private int[,] Maze;
    private List<Vector3> pathMazes = new List<Vector3>();
    private Stack<Vector2> _tiletoTry = new Stack<Vector2>();
    private List<Vector2> offsets = new List<Vector2> {
        new Vector2(0, 1), new Vector2(0, -1),
        new Vector2(1, 0), new Vector2(-1, 0)
    };

    private System.Random rnd = new System.Random();
    private Vector2 _currentTile;

    public Vector2 CurrentTile
    {
        get { return _currentTile; }
        private set
        {
            if (value.x < 1 || value.x >= this.width - 1 || value.y < 1 || value.y >= this.height - 1)
                throw new ArgumentException("CurrentTile must be within the one tile border all around the maze");

            if ((int)value.x % 2 == 1 || (int)value.y % 2 == 1)
                _currentTile = value;
            else
                throw new ArgumentException("Current tile must not be both even X and even Y");
        }
    }

    private static MazeGenerator instance;
    public static MazeGenerator Instance => instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = height / 2f + 5;
        GenerateMaze();
    }

    void GenerateMaze()
    {
        Maze = new int[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                Maze[x, y] = 1;

        CurrentTile = Vector2.one;
        _tiletoTry.Push(CurrentTile);
        Maze = CreateMaze();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Maze[i, j] == 1)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(i, j, 0);
                    if (brick != null)
                    {
                        Renderer rend = cube.GetComponent<Renderer>();
                        if (rend != null)
                            rend.material = brick;
                    }
                    cube.transform.parent = transform;
                }
                else
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }
            }
        }
    }

    public int[,] CreateMaze()
    {
        List<Vector2> neighbors;

        while (_tiletoTry.Count > 0)
        {
            Maze[(int)CurrentTile.x, (int)CurrentTile.y] = 0;
            neighbors = GetValidNeighbors(CurrentTile);

            if (neighbors.Count > 0)
            {
                _tiletoTry.Push(CurrentTile);
                CurrentTile = neighbors[rnd.Next(neighbors.Count)];
            }
            else
            {
                CurrentTile = _tiletoTry.Pop();
            }
        }

        return Maze;
    }

    private List<Vector2> GetValidNeighbors(Vector2 centerTile)
    {
        List<Vector2> validNeighbors = new List<Vector2>();

        foreach (var offset in offsets)
        {
            Vector2 toCheck = centerTile + offset;

            if ((int)toCheck.x % 2 == 1 || (int)toCheck.y % 2 == 1)
            {
                if (IsInside(toCheck) && Maze[(int)toCheck.x, (int)toCheck.y] == 1 && HasThreeWallsIntact(toCheck))
                {
                    validNeighbors.Add(toCheck);
                }
            }
        }

        return validNeighbors;
    }

    private bool HasThreeWallsIntact(Vector2 tile)
    {
        int intactWallCounter = 0;

        foreach (var offset in offsets)
        {
            Vector2 neighbor = tile + offset;
            if (IsInside(neighbor) && Maze[(int)neighbor.x, (int)neighbor.y] == 1)
            {
                intactWallCounter++;
            }
        }

        return intactWallCounter == 3;
    }

    private bool IsInside(Vector2 p)
    {
        return p.x >= 0 && p.y >= 0 && p.x < width && p.y < height;
    }
}
