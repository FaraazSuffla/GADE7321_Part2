using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuProgression : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject ModeSelectPanel;
    public GameObject DifficultySelectPanel;

    public void Play()
    {
        MainMenuPanel.SetActive(false);
        ModeSelectPanel.SetActive(true);
    }

    public void ModeSelect()
    {
        ModeSelectPanel.SetActive(false);
        DifficultySelectPanel.SetActive(true);
    }

    public void ReturnToPlay()
    {
        ModeSelectPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void ReturnToModeSelect()
    {
        DifficultySelectPanel.SetActive(false);
        ModeSelectPanel.SetActive(true);
    }

    public void NormalDifficulty()
    {
        SceneManager.LoadScene("DefaultMap (Normal AI)");
        
    }
    public void ChallengeDifficulty()
    {
        
        SceneManager.LoadScene("DefaultMap (Difficult AI)");
    }

    public void VSHuman()
    {
        SceneManager.LoadScene("DefaultMap");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
