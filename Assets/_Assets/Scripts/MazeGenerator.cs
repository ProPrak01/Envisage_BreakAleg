using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    public GameObject wallPrefab; // Prefab of a wall

    private int[,] mazeGrid;

    void Start()
    {
        GenerateMaze();
        CreateMaze3D();
    }

    void GenerateMaze()
    {
        mazeGrid = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                mazeGrid[i, j] = 1; // Initialize with walls
            }
        }

        int startX = Random.Range(0, width);
        int startY = Random.Range(0, height);

        mazeGrid[startX, startY] = 0; // Mark the starting cell as visited

        while (true)
        {
            int[] neighbors = GetUnvisitedNeighbors(startX, startY);

            if (neighbors.Length > 0)
            {
                int randomNeighbor = Random.Range(0, neighbors.Length);
                int nx = neighbors[randomNeighbor] / 100;
                int ny = neighbors[randomNeighbor] % 100;
                mazeGrid[nx, ny] = 0; // Carve a passage
                mazeGrid[startX + (nx - startX) / 2, startY + (ny - startY) / 2] = 0; // Remove wall between current and next cell
                startX = nx;
                startY = ny;
            }
            else
            {
                break;
            }
        }
    }

    int[] GetUnvisitedNeighbors(int x, int y)
    {
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };
        int[] neighbors = new int[4];
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i] * 2;
            int ny = y + dy[i] * 2;

            if (nx > 0 && nx < width && ny > 0 && ny < height && mazeGrid[nx, ny] == 1)
            {
                neighbors[count] = nx * 100 + ny;
                count++;
            }
        }

        return neighbors;
    }

    void CreateMaze3D()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (mazeGrid[i, j] == 1)
                {
                    Vector3 position = new Vector3(i, 0.5f, j); // Adjust Y to position the walls correctly
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
            }
        }
    }
}
