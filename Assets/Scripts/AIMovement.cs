using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AIMovement : MonoBehaviour
{
    public Color playerColor = Color.green;
    public float moveSpeed = 5f;
    public bool isMoving = false;
    public Vector3 targetPosition;
    private Renderer playerRenderer;

    private GameObject exitObject;
    private int maxDepth = 3; // Maximum depth for minimax search

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.color = playerColor;

        // Find the exit object tagged "Exit"
        exitObject = GameObject.FindGameObjectWithTag("Exit");
    }

    // Function called to make the AI move
    public void MakeMove()
    {
        if (GameManagerAI.instance && !isMoving)
        {
            Vector3 bestMove = Minimax(gameObject.transform.position, maxDepth, true);
            if (bestMove != Vector3.zero)
            {
                Move(bestMove);
            }
        }
    }

    // Function to move the AI
    private void Move(Vector3 direction)
    {
        targetPosition = transform.position + direction;
        if (IsTargetPositionValid(targetPosition))
        {
            isMoving = true;
            StartCoroutine(MoveToTarget(targetPosition));
        }
    }

    // Coroutine to move the AI smoothly
    private IEnumerator MoveToTarget(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        GameManagerAI.instance.SwitchTurn();
    }

    // Function to check if the target position is valid
    private bool IsTargetPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Wall")) return false;
        }
        return true;
    }

    // Minimax algorithm with alpha-beta pruning
    private Vector3 Minimax(Vector3 currentPosition, int depth, bool maximizingPlayer)
    {
        if (depth == 0)
        {
            return Vector3.zero; // Return zero vector at leaf nodes
        }

        Vector3 bestMove = Vector3.zero;
        int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;
        Vector3[] possibleMoves = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        foreach (Vector3 move in possibleMoves)
        {
            Vector3 nextPosition = currentPosition + move;
            if (IsTargetPositionValid(nextPosition))
            {
                int score = EvaluateMove(nextPosition, depth, maximizingPlayer);
                if (maximizingPlayer)
                {
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                }
                else
                {
                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                }
            }
        }

        return bestMove;
    }

    private int EvaluateMove(Vector3 position, int depth, bool maximizingPlayer)
    {
        // Ensure that the exitObject is not null before attempting to access its transform
        if (exitObject == null)
        {
            Debug.LogError("Exit object is not assigned in AIMovement script.");
            return 0; // Return a default value to handle the error gracefully
        }

        // Evaluate the desirability of the move based on distance to the exit
        float distanceToExit = Vector3.Distance(position, exitObject.transform.position);

        if (depth == 0 || distanceToExit == 0) // Leaf node or reaching exit
        {
            return Mathf.RoundToInt(1000 / (depth + 1)); // Reward for reaching exit, considering depth
        }

        // Define possible moves (up, down, left, right)
        Vector3[] possibleMoves = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        // Additional weight for moves closer to the exit
        int distanceWeight = 100; // Adjust as needed
        int score = 0;

        foreach (Vector3 move in possibleMoves)
        {
            Vector3 nextPosition = position + move;

            // Check if the target position is valid (not obstructed by a wall)
            if (IsTargetPositionValid(nextPosition))
            {
                float nextDistanceToExit = Vector3.Distance(nextPosition, exitObject.transform.position);
                int moveScore = Mathf.RoundToInt(distanceWeight / nextDistanceToExit);
                score += moveScore;
            }
        }

        return score;
    }


}
