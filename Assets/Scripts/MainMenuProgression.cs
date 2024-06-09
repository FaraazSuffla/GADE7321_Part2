using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuProgression : MonoBehaviour
{
    public GameObject MapSelectPanel;
    public GameObject ModeSelectPanel;
    public GameObject DifficultySelectPanel;

    public void MapSelect()
    {
        MapSelectPanel.SetActive(false);
        ModeSelectPanel.SetActive(true);
    }

    public void ModeSelect()
    {
        ModeSelectPanel.SetActive(false);
        DifficultySelectPanel.SetActive(true);
    }

    public void DifficultySelect()
    {
        SceneManager.LoadScene("DefaultMap (Normal AI)");
        SceneManager.LoadScene("DefaultMap (Difficult AI)");
    }
}
