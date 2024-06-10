
using UnityEngine;
using UnityEngine.UI;

public class GameManagerAI : MonoBehaviour
{
    public static GameManagerAI instance;

    public Player1MovementAI player1Movement;
    public AIMovement aiMovement;
    public Text turnIndicatorText;

    private bool isPlayer1Turn = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("GameManagerAI Start");
        StartPlayer1Turn();
    }

    void StartPlayer1Turn()
    {
        Debug.Log("Starting Player 1's Turn");

        if (player1Movement == null)
        {
            Debug.LogError("Player1Movement is not assigned in GameManagerAI");
            return;
        }

        if (turnIndicatorText == null)
        {
            Debug.LogError("TurnIndicatorText is not assigned in GameManagerAI");
            return;
        }

        player1Movement.enabled = true;
        aiMovement.enabled = false;
        turnIndicatorText.text = "Player 1's Turn";
    }

    void StartAITurn()
    {
        Debug.Log("Starting AI's Turn");

        if (aiMovement == null)
        {
            Debug.LogError("AIMovement is not assigned in GameManagerAI");
            return;
        }

        if (turnIndicatorText == null)
        {
            Debug.LogError("TurnIndicatorText is not assigned in GameManagerAI");
            return;
        }

        player1Movement.enabled = false;
        aiMovement.enabled = true;
        aiMovement.MakeMove();
        turnIndicatorText.text = "AI's Turn";
    }

    public void SwitchTurn()
    {
        Debug.Log("Switching Turn");
        isPlayer1Turn = !isPlayer1Turn;
        if (isPlayer1Turn)
        {
            StartPlayer1Turn();
        }
        else
        {
            StartAITurn();
        }
    }
}
