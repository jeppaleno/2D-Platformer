using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public static bool isLevelComplete;
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;

    private void Awake()
    {
        isGameOver = false;
        isLevelComplete = false;
    }

    private void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
        if (isLevelComplete)
        {
            levelCompleteScreen.SetActive(true);
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
