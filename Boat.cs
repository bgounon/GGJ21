using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed = 2f;
    private bool moving;
    private Rigidbody2D rb2d;
    private Vector2 velocity;


    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.right * speed;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            rb2d.velocity = velocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player")
        {
           moving = true;
            print("The boat sails!");
        }
    }
}
