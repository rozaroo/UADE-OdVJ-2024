using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] GameObject victoryScreen;
    Ball ball;
    public int lives = 3;

    private void Start()
    {
        ball = Ball.Instance;
    }
    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Exit();
        if (Input.GetKeyUp(KeyCode.R)) ResetGame();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (lives > 0) ball.LaunchBall();
        }
        CheckLevelCompleted();

    }
    public void LoseHealth()
    {
        lives--;
        if (lives <= 0) DefeatScreen();
        else ResetLevel();
    }
    public void ResetLevel()
    {
        ball.ResetBall();
        FindObjectOfType<Player>().ResetPlayer();
    }
    public void ResetGame()
    {
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
        lives += 3;
        ActivateAllChildren();
    }
    public void ActivateAllChildren()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    public void CheckLevelCompleted()
    {
        bool allObjectsInactive = true;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                allObjectsInactive = false;
                break;
            }
        }
        if (allObjectsInactive) VictoryScreen();
    }
    public void DefeatScreen()
    {
        Time.timeScale = 0f;
        if (defeatScreen == null) return;
        defeatScreen.SetActive(true);
    }
    public void VictoryScreen()
    {
        Time.timeScale = 0f;
        if (victoryScreen == null) return;
        victoryScreen.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
