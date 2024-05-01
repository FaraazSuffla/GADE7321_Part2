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

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        // Generate a random color for walls and floors
        Color wallColor = Random.ColorHSV();
        Color floorColor = Random.ColorHSV();

        // Loop through each position in the grid
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeHeight; z++)
            {
                // Calculate the position for the current cube
                Vector3 position = new Vector3(x, 0, z);

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
