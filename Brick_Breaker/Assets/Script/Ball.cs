using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importing for UI
using UnityEngine.SceneManagement; // // Importing for SceneManagement

public class Ball : MonoBehaviour
{
    
    // Initializing and Declaring Fields
    private float x; 
    private int Lives = 3;
    public static int Score = 0; 
    private Vector2 force; //Vector2 Variable
    private Rigidbody2D rb; //RigidBody Variable
    public Transform explosion; // Transform Variable
    
    [SerializeField] private float movespeed = 3f; 
    [SerializeField] private AudioSource WallSoundEffect; // Wall Sound
    [SerializeField] private AudioSource PedalSoundEffect; // Pedal Sound
    [SerializeField] private AudioSource BrickSoundEffect; // Brick Sound
    [SerializeField] private Text ScoreText; //Text Variable that Visible in Unity Engine[SerializeField]
    [SerializeField] private Text LivesText;  //Text Variable that Visible in Unity Engine[SerializeField]

    void Start()
    {   
        // When Game Start this code will work for First frame 
 
        rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2();
        //For Random RAnge Max=1f and Min=-1f
        force.x = Random.Range(-1f, 1f); 
        force.y = -1f;
        // force with Normalized Vector - keeping it pointing in the same direction, change its length to 1.
        rb.AddForce(force.normalized * movespeed); 
    } 

    void Update()
    {   
        // This code will update in each Frame
         ScoreText.text = "Score: " + Score;
        x += Time.deltaTime; 
        if(x > 3f) // for slowmotion
        {
        rb.velocity = rb.velocity.normalized * movespeed; 
        }   
        
    }
   
    //Function for Pedal Collision
    private void Pedal(Collision2D Collider)
    {
        if(Collider.gameObject.tag == "Pedal")
        {
            PedalSoundEffect.Play();
        }
    }


    //Function for Brick Collision
    private void Brick(Collision2D Collider)
    {
        if(Collider.gameObject.tag == "Brick")
        {
            Destroy(Collider.gameObject); //Destory Brick
            // for partile Instantiate(gameobject,position,rotation)
            Transform newExplosion = Instantiate(explosion, Collider.transform.position, Collider.transform.rotation); 
            Destroy(newExplosion.gameObject,2.5f); // Destory(gameobject,Time limit)
            BrickSoundEffect.Play();
            Score++;
            ScoreText.text = "Score: " + Score;
        }
    }


    //Function for Dangerous_Wall Collision
    private void Dangerous_Wall(Collision2D Collider)
    {
        if(Collider.gameObject.tag == "Dangerous_Wall")
        {
            
            if(Lives == 1)
            {
                SceneManager.LoadScene("Game_Over"); // Loading Game_Over Scene
            }
            else
            {
            Lives--;
            LivesText.text = "Lives: " + Lives;
            WallSoundEffect.Play();
            }
        }
    }


    // Function For Collision
    private void OnCollisionEnter2D(Collision2D Collider)
    {
        Brick(Collider); // Brick Collision Function Call
        Pedal(Collider); // Pedal Collision Function Call
        Dangerous_Wall(Collider); // Dangerous_Wall Collision Function Call
    }
}
