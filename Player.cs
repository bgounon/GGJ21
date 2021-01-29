using System.Collections;
using System.Collections.Generic;
using UnityEditor.CrashReporting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    private bool moving;
    private Rigidbody2D rb2d;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        velocity = transform.right * speed;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) == true) {
            moving = true;
            print("Space key pressed.");
        }
    
        if (moving) {
            rb2d.velocity = velocity;
        }

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Finish") {
            GameManager.PlayerTriggerFinish();
        }
        if (col.gameObject.tag == "Death") {
            GameManager.PlayerTriggerDeath();
        }
        if (col.gameObject.tag == "Boat") {
            GameManager.PlayerTriggerBoat(col.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Obstacle") {
            CollisionWithObstacle(col);
        }
    }

    void CollisionWithObstacle(Collision2D col){
        print("Collide with obstacle.");

        Vector2 inNormal = col.contacts[0].normal;
        Vector2 newVelocity = Vector2.Reflect(velocity, inNormal);
        print(newVelocity);
        velocity = newVelocity;
    }

}
