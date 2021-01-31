using System.Collections;
using System.Collections.Generic;
using UnityEditor.CrashReporting;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public GameObject spawnPoint;
    public float speed = 2f;
    protected bool moving;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected GameManager manager;
    protected Sound sound;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        manager = FindObjectOfType<GameManager>();
        rb2d = GetComponent<Rigidbody2D>();
        moving = false;
        reset();
        sound = FindObjectOfType<Sound>();
    }

    public void startMoving()
    {
        moving = true;
        velocity = transform.right * speed;
    }

    public void stopMoving()
    {
        moving = false;
        rb2d.velocity = Vector2.zero;
    }

    public virtual void reset()
    {
        stopMoving();
        transform.rotation = Quaternion.identity;
        transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (moving) {
            rb2d.velocity = velocity;
        }

    }

    protected virtual void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Obstacle") {
            
            CollisionWithObstacle(col);
        }
    }

    protected void CollisionWithObstacle(Collision2D col){
        print("Collide with obstacle.");
        sound.obstacleHitSound();

        Vector2 inNormal = col.contacts[0].normal;
        Vector2 newVelocity = Vector2.Reflect(velocity, inNormal);
        velocity = newVelocity;
    }
    public Vector2 getDirection()
    {
        return velocity;
    }

}
