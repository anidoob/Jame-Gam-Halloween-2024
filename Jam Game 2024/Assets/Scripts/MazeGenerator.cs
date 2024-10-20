using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Range(5, 500)]
    public int mazeWidth = 5, mazeHeight = 5; //Dimension of the maze

    public int startX, startY; // Starting position of the algorithm

    MazeCell[,] maze;

    Vector2Int currentCell;
    public void Start()
    {
        maze = new MazeCell[mazeWidth, mazeHeight]; //Create a new maze with the given dimensions

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = new MazeCell(x, y); //Create MazeCell for each position in the maze
            }
        }
    }
    List<Direction> directions = new List<Direction>
    {
        Direction.Up,
        Direction.Down,
        Direction.Left,
        Direction.Right
    };

    List<Direction> GetRandomDirections()
    {
        List<Direction> dir = new List<Direction>(directions);

        List<Direction> randomDirections = new List<Direction>();
        while (dir.Count > 0)
        {
            int rnd = Random.Range(0, dir.Count);
            randomDirections.Add(dir[rnd]);
            dir.RemoveAt(rnd);
        }

        return randomDirections;

    }

    bool IsCellValid(int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1|| y > mazeHeight -1 || maze[x,y].visited)
        {
            return false;
        }
        return true;
    }

    Vector2Int CheckNeighbour()
    {
        List<Direction> randomDirections = GetRandomDirections();

        for (int i = 0; i < randomDirections.Count; i++)
        {
            Vector2Int neighbour = currentCell;

            switch (randomDirections[i])
            {
                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
            }
            if(IsCellValid(neighbour.x, neighbour.y)){
                return neighbour;
            }
        }
        return currentCell;
    }

    void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if(primaryCell.x > secondaryCell.x)//If the primary cell is to the right of the secondary cell then break the left wall of the primary cell
        { 
            maze[primaryCell.x,primaryCell.y].leftWall = false;
        }
        else if (primaryCell.x < secondaryCell.x)//If the primary cell is to the left of the secondary cell then break the left wall of the secondary cell
        {
            maze[secondaryCell.x, secondaryCell.y].leftWall = false;
        }
        else if (primaryCell.y > secondaryCell.y)//If the primary cell is above the secondary cell then break the top wall of the secondary cell
        {
            maze[secondaryCell.x, secondaryCell.y].topWall = false;
        }
        else if (primaryCell.y < secondaryCell.y)//If the primary cell is below the secondary cell then break the top wall of the primary cell
        {
            maze[primaryCell.x, primaryCell.y].topWall = false;
        }
    }

    void carvePath(int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1)
        {
            x = y = 0;
            Debug.Log("Invalid starting coordinates for carving path");
        }
    }

public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class MazeCell
    {
        public bool visited;
        public int x;
        public int y;
        public bool topWall;
        public bool leftWall;

        public Vector2Int position
        {
            get
            {

                return new Vector2Int(x, y);

            }
        }

        public MazeCell(int x, int y)
        {
            // The coordinates of the cell
            this.x = x;
            this.y = y;

            //Whether the cell has been visited - false on start
            visited = false;

            //All walls are present on start
            topWall = leftWall = true;
        }
    }
}
