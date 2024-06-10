using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1MovementAI : MonoBehaviour
{
    public Color playerColor = Color.blue;
    public float moveSpeed = 5f;
    public bool isMoving = false;
    public Vector3 targetPosition;
    private Renderer playerRenderer;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.color = playerColor;
    }

    void Update()
    {
        if (GameManagerAI.instance && GameManagerAI.instance.player1Movement == this && !isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W)) Move(Vector3.forward);
            if (Input.GetKeyDown(KeyCode.S)) Move(Vector3.back);
            if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.left);
            if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.right);
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
}
