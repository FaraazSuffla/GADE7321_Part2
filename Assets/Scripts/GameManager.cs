using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of the GameManager

    // References to Player1Movement and Player2Movement scripts
    public Player1Movement player1Movement;
    public Player2Movement player2Movement;

    private bool isPlayer1Turn = true; // Flag to track Player 1's turn

    void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        StartPlayer1Turn(); // Start the game with Player 1's turn
    }

    void StartPlayer1Turn()
    {
        // Enable Player 1's movement and disable Player 2's movement
        player1Movement.enabled = true;
        player2Movement.enabled = false;
    }

    void StartPlayer2Turn()
    {
        // Enable Player 2's movement and disable Player 1's movement
        player1Movement.enabled = false;
        player2Movement.enabled = true;
    }

    // Method to switch turns between players
    public void SwitchTurn()
    {
        isPlayer1Turn = !isPlayer1Turn; // Switch turns
        if (isPlayer1Turn)
            StartPlayer1Turn(); // Start Player 1's turn
        else
            StartPlayer2Turn(); // Start Player 2's turn
    }
}
