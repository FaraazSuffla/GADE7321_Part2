using UnityEngine;
using System.Collections;

public class Player1Movement : MonoBehaviour
{
    public Color playerColor = Color.blue; // Color for Player 1
    public float moveSpeed = 5f;
    public bool isMoving = false;
    public Vector3 targetPosition;
    private Renderer playerRenderer; // Renderer component of the player GameObject

    void Start()
    {
        // Get the Renderer component attached to the player GameObject
        playerRenderer = GetComponent<Renderer>();
        // Set the initial color of the player GameObject
        playerRenderer.material.color = playerColor;
    }

    void Update()
    {
        // Allow movement only when it's Player 1's turn
        if (GameManager.instance && GameManager.instance.player1Movement == this && !isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector3.back);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector3.right);
            }
        }
    }

    private void Move(Vector3 direction)
    {
        targetPosition = transform.position + direction;

        if (IsTargetPositionValid(targetPosition))
        {
            isMoving = true;
            StartCoroutine(MoveToTarget(targetPosition));
        }
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        GameManager.instance.SwitchTurn(); // Notify the GameManager to switch turns
    }

    private bool IsTargetPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f); // Adjust the radius as needed

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                return false; // Target position is inside a wall, movement is invalid
            }
        }

        return true; // Target position is not inside any wall, movement is valid
    }
}
