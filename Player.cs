using System.Collections;
using System.Collections.Generic;
using UnityEditor.CrashReporting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject spawnPoint;
    public float speed = 2f;
    private bool moving;
    private Rigidbody2D rb2d;
    private Vector2 velocity;
    private GameManager manager;
    private bool sailing;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        rb2d = GetComponent<Rigidbody2D>();
        moving = false;
        sailing = false;
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

    public void resetPlayer()
    {
        sailing = false;
        transform.rotation = Quaternion.identity;
        transform.position = spawnPoint.transform.position;
    }

    public void sail() {
        sailing = true;
    }

    public void stopSail() {
        sailing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            rb2d.velocity = velocity;
        }

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Finish") {
            manager.PlayerTriggerFinish();
        }
        if (col.gameObject.tag == "Death") {
            manager.PlayerTriggerDeath();
        }
        if (col.gameObject.tag == "Coin") {
            Coin coin = col.gameObject.GetComponent<Coin>();
            manager.addScore(coin.value);
            Destroy(coin.gameObject);
        }

    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Island" && sailing == false && !rb2d.isKinematic) {
            manager.PlayerTriggerDrown();
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
