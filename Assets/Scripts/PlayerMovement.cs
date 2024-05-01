using UnityEngine;
using System.Collections; // Add this line

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control the player's movement speed
    public bool isMoving = false; // Flag to check if the player is currently moving
    public Vector3 targetPosition; // The position the player is moving towards

    private void Update()
    {
        if (!isMoving)
        {
            // Check for input to move the player
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector3.forward); // Move up
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector3.back); // Move down
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector3.left); // Move left
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector3.right); // Move right
            }
        }
    }

    // Function to move the player in a specific direction
    private void Move(Vector3 direction)
    {
        // Calculate the target position
        targetPosition = transform.position + direction;

        // Check if the target position is within the grid boundaries
        if (IsTargetPositionValid(targetPosition))
        {
            // Set the flag to indicate the player is moving
            isMoving = true;

            // Move the player towards the target position
            StartCoroutine(MoveToTarget(targetPosition));
        }
    }

    // Coroutine to smoothly move the player towards the target position
    private IEnumerator MoveToTarget(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Set the flag to indicate the player has finished moving
        isMoving = false;
    }

    // Function to check if the target position is within the grid boundaries
    private bool IsTargetPositionValid(Vector3 position)
    {
        // Implement your logic to check if the target position is within the grid boundaries
        // You can use the maze dimensions or any other criteria to determine validity
        // Return true if the target position is valid, false otherwise
        // You can customize this function based on your maze design
        return true; // Placeholder - replace this with your actual implementation
    }
}
