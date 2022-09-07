using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_3 : MonoBehaviour
{
    private float x; 
    void Start()
    {
        Ball.Score = 0;
    }
    

    void Update()
    {
        if(Ball.Score >= 60)
        {
            x += Time.deltaTime;
            if(x > 3f)
        {
        SceneManager.LoadScene("Complete_Game"); //Loading Complete_Game
        } 
        } 
    }
}
