using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Settings")]
    public int mazeWidth = 10; // Number of cells in the X direction
    public int mazeHeight = 10; // Number of cells in the Z direction
    public GameObject wallPrefab; // Reference to the wall prefab
    public GameObject floorPrefab; // Reference to the floor prefab

    [Header("Maze Generation Settings")]
    [Range(0f, 1f)]
    public float wallProbability = 0.3f; // Probability of a cell being a wall
    public Color wallColor = Color.gray; // Color of the walls
    public Color floorColor = Color.white; // Color of the floors

    private List<List<GameObject>> mazeCells = new List<List<GameObject>>(); // 2D array to store maze GameObjects

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        for (int x = 0; x < mazeWidth; x++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int z = 0; z < mazeHeight; z++)
            {
                GameObject cell = Instantiate(wallPrefab, new Vector3(x, 0, z), Quaternion.identity);
                row.Add(cell);
            }
            mazeCells.Add(row);
        }

        // Create floors
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeHeight; z++)
            {
                GameObject floor = Instantiate(floorPrefab, new Vector3(x, -0.5f, z), Quaternion.identity);
                floor.GetComponent<Renderer>().material.color = floorColor;
            }
        }

        // Generate maze paths
        DFS(1, 1);

        // Open entrance and exit
        Destroy(mazeCells[0][1]); // Remove wall for entrance
        Destroy(mazeCells[mazeWidth - 1][mazeHeight - 2]); // Remove wall for exit
    }

    void DFS(int x, int z)
    {
        mazeCells[x][z].SetActive(false); // Mark cell as part of the path

        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, 1), // Up
            new Vector2Int(0, -1), // Down
            new Vector2Int(1, 0), // Right
            new Vector2Int(-1, 0) // Left
        };

        Shuffle(directions);

        foreach (Vector2Int dir in directions)
        {
            int nextX = x + dir.x * 2;
            int nextZ = z + dir.y * 2;

            if (nextX > 0 && nextX < mazeWidth && nextZ > 0 && nextZ < mazeHeight && mazeCells[nextX][nextZ].activeSelf)
            {
                mazeCells[nextX - dir.x][nextZ - dir.y].SetActive(false); // Remove wall between cells
                DFS(nextX, nextZ);
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
