using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour
{
    public Color playerColor = Color.green;
    public float moveSpeed = 5f;
    public bool isMoving = false;
    public Vector3 targetPosition;
    private Renderer playerRenderer;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.color = playerColor;
    }

    public void MakeMove()
    {
        if (GameManagerAI.instance && !isMoving)
        {
            Vector3 bestMove = FindBestMove();
            if (bestMove != Vector3.zero)
            {
                Move(bestMove);
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
        GameManagerAI.instance.SwitchTurn();
    }

    private bool IsTargetPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Wall")) return false;
        }
        return true;
    }

    private Vector3 FindBestMove()
    {
        // Placeholder: Replace with actual Minimax algorithm
        Vector3[] possibleMoves = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        Vector3 bestMove = Vector3.zero;
        int bestScore = int.MinValue;

        foreach (Vector3 move in possibleMoves)
        {
            Vector3 potentialPosition = transform.position + move;
            if (IsTargetPositionValid(potentialPosition))
            {
                int moveScore = EvaluateMove(potentialPosition);
                if (moveScore > bestScore)
                {
                    bestScore = moveScore;
                    bestMove = move;
                }
            }
        }

        return bestMove;
    }

    private int EvaluateMove(Vector3 position)
    {
        // Placeholder for move evaluation logic
        return Random.Range(0, 10); // Replace with actual evaluation logic
    }
}
