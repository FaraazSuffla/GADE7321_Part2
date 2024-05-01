using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int mazeWidth = 10; // Number of cubes in the X direction
    public int mazeHeight = 10; // Number of cubes in the Z direction
    public float wallHeight = 2f; // Height of the walls
    public GameObject wallPrefab; // Reference to the wall prefab
    public GameObject floorPrefab; // Reference to the floor prefab
    public Vector3 startOffset = new Vector3(0.5f, 0.5f, 0.5f); // Offset to align the maze properly

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        // Loop through each position in the grid
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeHeight; z++)
            {
                // Calculate the position for the current cube
                Vector3 position = new Vector3(x, 0, z);

                // Instantiate the floor cube at the calculated position
                GameObject floor = Instantiate(floorPrefab, position + startOffset, Quaternion.identity);
                floor.transform.parent = transform; // Set parent to the grid GameObject

                // If it's an edge cell, instantiate walls
                if (x == 0 || z == 0 || x == mazeWidth - 1 || z == mazeHeight - 1)
                {
                    Vector3 wallPosition = new Vector3(x, wallHeight / 2f, z); // Position for the wall
                    GameObject wall = Instantiate(wallPrefab, wallPosition + startOffset, Quaternion.identity);
                    wall.transform.localScale = new Vector3(1f, wallHeight, 1f); // Adjust scale to match wall height
                    wall.transform.parent = transform; // Set parent to the grid GameObject
                }

                // Set a random color for the cube
                Color randomColor = new Color(Random.value, Random.value, Random.value);
                floor.GetComponent<Renderer>().material.color = randomColor;
            }
        }
    }
}
