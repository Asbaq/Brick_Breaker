using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importing for UI
using UnityEngine.SceneManagement; // // Importing for SceneManagement

public class Ball : MonoBehaviour
{
    
    // Initializing and Declaring Fields
    [SerializeField] public ParticleSystem part;
    [SerializeField] private AudioSource WallSoundEffect; // Wall Sound
    [SerializeField] private AudioSource PedalSoundEffect; // Pedal Sound
    [SerializeField] private AudioSource BrickSoundEffect; // Brick Sound
    private Rigidbody2D rb; //RigidBody Variable
    private Vector2 force; //Vector2 Variable
    [SerializeField] private float movespeed = 5f; 
    public static int Score = 0; 
    [SerializeField] private Text ScoreText; //Text Variable that Visible in Unity Engine[SerializeField]
    private float x; 

    void Start()
    {   
        // When Game Start this code will work for First frame  
        rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2();
        force.x = Random.Range(-1f, 1f); //For Random RAnge Max=1f and Min=-1f
        force.y = -1f;
        rb.AddForce(force.normalized * movespeed); // force with Normalized Vector - keeping it pointing in the same direction, change its length to 1.
    }

    void Particle()
    {
        Debug.Log("sd");
        part.Play();
    } 

    void Update()
    {   
        // This code will update in each Frame
        x += Time.deltaTime; 
        if(x > 3f) // for slowmotion
        {
        rb.velocity = rb.velocity.normalized * movespeed; 
        }   
        
    }

   

    //Function for Brick Collision
    private void Brick(Collision2D Collider)
    {
        if(Collider.gameObject.tag == "Brick")
        {
            Destroy(Collider.gameObject); //Destory Brick
            // Particle();
            Debug.Log("HI");
            BrickSoundEffect.Play();
            Score++;
            ScoreText.text = "Score: " + Score;
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

    //Function for Dangerous_Wall Collision
    private void Dangerous_Wall(Collision2D Collider)
    {
        if(Collider.gameObject.tag == "Dangerous_Wall")
        {
            WallSoundEffect.Play();
            SceneManager.LoadScene("Game_Over"); // Loading Game_Over Scene
        }
    }

    // Function For Collision
    private void OnCollisionEnter2D(Collision2D Collider)
    {
        Dangerous_Wall(Collider); // Dangerous_Wall Collision Function Call
        Brick(Collider); // Brick Collision Function Call
        Pedal(Collider); // Pedal Collision Function Call
    }
}
