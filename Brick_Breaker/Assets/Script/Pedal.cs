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
        dirX = Input.GetAxisRaw("Horizontal"); // Horizontal/X-axis Movement <-Left Arrow/Right Arrow->
        rb.velocity = new Vector2(dirX * movespeed,rb.velocity.y); // Multiply with movespeed
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

            float Ball_Angle = Vector2.SignedAngle(Vector2.up, ball.GetComponent<Rigidbody2D>().velocity); // current Ball Angle used SignedAngle(Vector2 from,Vector2 to) for signed Value 
            float Ball_bounceAngle = (Pedal_offset / Pedal_maxOffset) * Ball_maxBounceAngle; // Ball bounce Angle should be Pedal offset/Pedal maxOffset *  Ball maxBounceAngle
            float Ball_newAngle = Mathf.Clamp(Ball_Angle + Ball_bounceAngle, -Ball_maxBounceAngle, Ball_maxBounceAngle); // Ball New Angle shoulde use Math.Clamp(New Angle Value,Min NewAngle value,Max NewAngle Value)
            // your angle can't be less than min Angle Value not be more than maximum Value

            Quaternion Angle_rotation = Quaternion.AngleAxis(Ball_newAngle, Vector3.forward);//In Unity Quaternions are used to represent rotations. 
            //They are generalize of two-dimensional complex numbers to three dimensions. AngleAxis(Angle Value,Vector3.direction).
            ball.GetComponent<Rigidbody2D>().velocity = Angle_rotation * Vector2.up * ball.GetComponent<Rigidbody2D>().velocity.magnitude;
            // for velocity of angle = angle_rotation * direction * speed of ball
        }
   }
}
