using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int mazeWidth = 10; // Number of cubes in the X direction
    public int mazeHeight = 10; // Number of cubes in the Z direction
    public float wallHeight = 2f; // Height of the walls
    public GameObject wallPrefab; // Reference to the wall prefab
    public GameObject floorPrefab; // Reference to the floor prefab
    public Vector3 startOffset = new Vector3(0.5f, 0.5f, 0.5f); // Offset to align the maze properly
    public float wallProbability = 0.3f; // Probability of a cube being a wall

    public Color wallColor = Color.gray; // Color of the walls
    public Color floorColor = Color.white; // Color of the floors

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

                // If it's an edge cell, instantiate a wall
                if (x == 0 || z == 0 || x == mazeWidth - 1 || z == mazeHeight - 1)
                {
                    InstantiateWall(position);
                }
                else
                {
                    // Decide whether to instantiate a wall or a floor
                    GameObject cubePrefab = (Random.value < wallProbability) ? wallPrefab : floorPrefab;

                    // Instantiate the cube (wall or floor) at the calculated position
                    GameObject cube = Instantiate(cubePrefab, position + startOffset, Quaternion.identity);
                    cube.transform.parent = transform; // Set parent to the grid GameObject

                    // If it's a wall, adjust the scale to match wall height and assign wall color
                    if (cubePrefab == wallPrefab)
                    {
                        cube.transform.localScale = new Vector3(1f, wallHeight, 1f);
                        cube.transform.position += new Vector3(0f, wallHeight / 2f, 0f); // Adjust position to raise the wall
                        AssignColor(cube, wallColor);
                    }
                    // If it's a floor, assign floor color
                    else
                    {
                        AssignColor(cube, floorColor);
                    }
                }
            }
        }
    }

    // Helper method to instantiate a wall at the specified position
    void InstantiateWall(Vector3 position)
    {
        GameObject wall = Instantiate(wallPrefab, position + startOffset, Quaternion.identity);
        wall.transform.parent = transform; // Set parent to the grid GameObject
        wall.transform.localScale = new Vector3(1f, wallHeight, 1f); // Adjust scale to match wall height
        AssignColor(wall, wallColor);
    }

    // Helper method to assign color to the cube's renderer
    void AssignColor(GameObject cube, Color color)
    {
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        if (cubeRenderer != null)
        {
            cubeRenderer.material.color = color; // Assign color
        }
    }
}
