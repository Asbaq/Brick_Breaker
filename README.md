# Brick_Breaker 🎮

![Brick_Breaker](https://user-images.githubusercontent.com/62818241/201542322-852164c9-e1e4-4866-a86f-c1191feae102.png)

## 📌 Introduction
**Brick_Breaker** is a classic arcade-style game where the player controls a paddle to bounce a ball and destroy bricks. The goal is to break all the bricks on each level while managing the ball's movements and avoiding dangerous walls. The game features smooth collision detection, dynamic scorekeeping, multiple levels, and engaging sound effects.

## 🔥 Features
- 🏓 **Paddle Control**: Move the paddle horizontally to bounce the ball.
- 🟩 **Brick Destruction**: Destroy bricks by hitting them with the ball.
- 💥 **Explosion Effects**: Visual effects when bricks are destroyed.
- 🧨 **Dangerous Walls**: Avoid hitting the dangerous walls, which reduce lives.
- 💖 **Lives System**: Player has a limited number of lives.
- 🎶 **Sound Effects**: Sound effects for brick destruction, paddle collision, and dangerous walls.
- 🏆 **Score Tracking**: Player earns points for each brick destroyed, and the score is displayed.
- 🎮 **Multiple Levels**: Progress through levels with increasing difficulty.

---

## 🏗️ How It Works

### 📌 **Ball Script**

The **Ball** script controls the behavior of the ball. It initializes the ball's movement, handles collisions with the paddle, bricks, and walls, and updates the score and lives.

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        //For Random Range Max=1f and Min=-1f
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
        if(x > 3f) // for slow motion
        {
            rb.velocity = rb.velocity.normalized * movespeed; 
        }   
    }
   
    // Function for Pedal Collision
    private void Pedal(Collision2D Collider)
    {
        if(Collider.gameObject.tag == "Pedal")
        {
            PedalSoundEffect.Play();
        }
    }

    // Function for Brick Collision
    private void Brick(Collision2D Collider)
    {
        if(Collider.gameObject.tag == "Brick")
        {
            Destroy(Collider.gameObject); // Destroy Brick
            // for particle Instantiate(gameobject,position,rotation)
            Transform newExplosion = Instantiate(explosion, Collider.transform.position, Collider.transform.rotation); 
            Destroy(newExplosion.gameObject, 2.5f); // Destroy the explosion after 2.5 seconds
            BrickSoundEffect.Play();
            Score++;
            ScoreText.text = "Score: " + Score;
        }
    }

    // Function for Dangerous Wall Collision
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
        Dangerous_Wall(Collider); // Dangerous Wall Collision Function Call
    }
}
```

### 📌 **Pedal Script**

The **Pedal** script controls the movement of the paddle. The paddle moves based on user input, and it also calculates the angle at which the ball bounces off the paddle.

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    // Initializing and Declaring Fields
    private Rigidbody2D rb;
    private float dirX = 0f;
    [SerializeField] private float movespeed = 10f;
    public float Ball_maxBounceAngle = 75f; // Initializing maxBounceAngle
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // This code will update in each Frame fixedUpdate() for Physics only
        dirX = Input.GetAxisRaw("Horizontal"); // Horizontal/X-axis Movement <-Left Arrow/Right Arrow->
        rb.velocity = new Vector2(dirX * movespeed, rb.velocity.y); // Multiply with movespeed
    }

    // Function For Collision with Ball for natural angle
    private void OnCollisionEnter2D(Collision2D ball_Collider)
    {
        Ball ball = ball_Collider.gameObject.GetComponent<Ball>(); // Fetching ball gameobject in Ball Variable

        if (ball != null) // if Ball Variable is not empty(succeed) 
        {
            Vector2 Position = transform.position; // Initializing Position by current Position of Pedal
            Vector2 Point = ball_Collider.GetContact(0).point; // Initializing Point from middle point of Pedal

            float Pedal_offset = Position.x - Point.x; // We will get the Offset by current Position x coordinate - middle point x coordinate 
            float Pedal_maxOffset = ball_Collider.otherCollider.bounds.size.x / 2; // We will the max_Offset of Pedal by full size x coordinates / 2 

            float Ball_Angle = Vector2.SignedAngle(Vector2.up, ball.GetComponent<Rigidbody2D>().velocity); // current Ball Angle used SignedAngle(Vector2 from, Vector2 to) for signed Value 
            float Ball_bounceAngle = (Pedal_offset / Pedal_maxOffset) * Ball_maxBounceAngle; // Ball bounce Angle should be Pedal offset/Pedal maxOffset *  Ball maxBounceAngle
            float Ball_newAngle = Mathf.Clamp(Ball_Angle + Ball_bounceAngle, -Ball_maxBounceAngle, Ball_maxBounceAngle); // Ball New Angle should use Math.Clamp(New Angle Value, Min NewAngle value, Max NewAngle Value)

            Quaternion Angle_rotation = Quaternion.AngleAxis(Ball_newAngle, Vector3.forward); // In Unity Quaternions are used to represent rotations. 
            ball.GetComponent<Rigidbody2D>().velocity = Angle_rotation * Vector2.up * ball.GetComponent<Rigidbody2D>().velocity.magnitude; // for velocity of angle = angle_rotation * direction * speed of ball
        }
    }
}
```

---

## 🎯 Conclusion
**Brick_Breaker** is an engaging, challenging game with multiple levels and increasing difficulty. It features smooth paddle and ball mechanics, brick destruction effects, and an exciting progression system. Players must avoid dangerous walls and manage their lives as they break bricks and earn points. The game’s simple yet addictive gameplay makes it a fun experience for all ages.
