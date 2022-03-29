using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;


    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public static bool GamePause = false;

    public bool isDead;
    public bool isWin;

    public float restartDelay = 2f;

    public GameObject pauseMenuUI;
    public GameObject winMenuUI;
    public GameObject restartMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        isDead = false;
        isWin = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        WinGame();
        LoseGame();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
    }

    public void NextScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void Restart()
    {
        Time.timeScale = 1f;
        Invoke("RestartGame", restartDelay);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void WinGame()
    {
        if(isWin == true)
        {
            winMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void LoseGame()
    {
        if(isDead == true)
        {
            restartMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
