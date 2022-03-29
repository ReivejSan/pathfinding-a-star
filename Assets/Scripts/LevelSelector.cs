using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
   public void LevelA()
   {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
   }

    public void LevelB()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void LevelC()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }
}

