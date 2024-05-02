using UnityEngine;
using System.Collections;

public class Player2Movement : MonoBehaviour
{
    public Color playerColor = Color.red; // Color for Player 2
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
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector3.back);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
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
