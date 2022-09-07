using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1"); //Loading Level_1
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); // Quit or End the Application
        Application.Quit();
    }
}
