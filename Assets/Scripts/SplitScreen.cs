using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    public Camera player1Camera;
    public Camera player2Camera;

    void Start()
    {
        // Set up player 1 camera
        player1Camera.rect = new Rect(0f, 0f, 0.5f, 1f); // Left half of the screen
        player1Camera.depth = 0; // Ensure player 1 camera renders in front

        // Set up player 2 camera
        player2Camera.rect = new Rect(0.5f, 0f, 0.5f, 1f); // Right half of the screen
        player2Camera.depth = -1; // Ensure player 2 camera renders behind player 1
    }
}
