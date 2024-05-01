using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int mazeWidth = 10; // Number of cubes in the X direction
    public int mazeHeight = 10; // Number of cubes in the Z direction
    public GameObject cubePrefab; // Reference to the cube prefab
    public Vector3 startOffset = new Vector3(0.5f, 0f, 0.5f); // Offset to align the maze properly

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

                // Instantiate a cube at the calculated position
                GameObject cube = Instantiate(cubePrefab, position + startOffset, Quaternion.identity);

                // Set the cube's parent to the Grid GameObject
                cube.transform.parent = transform;
            }
        }
    }
}
