using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_1 : MonoBehaviour
{
    private float x; 

    void Start()
    {
        Ball.Score = 0;
    }

    void Update()
    {
        if(Ball.Score >= 36)
        {
            x += Time.deltaTime;

        if(x > 3f)
        {
        SceneManager.LoadScene("Level_1_UI"); //Loading Level_1_UI
        }   
        } 
    }
}
