using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_2 : MonoBehaviour
{   
    private float x; 
    void Start()
    {
        Ball.Score = 0;
    }

    void Update()
    {
        if(Ball.Score >= 48)
        {
        x += Time.deltaTime;

        if(x > 3f)
        {
        SceneManager.LoadScene("Level_2_UI"); //Loading Level_2_UI
        }   
        }   
    }
}
