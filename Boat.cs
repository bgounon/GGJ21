using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boat : MonoBehaviour
{
    public GameObject spawnPoint;
    public float speed = 2f;
    private bool moving, sailing;
    private Rigidbody2D rb2d;
    private Vector2 velocity;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        velocity = transform.right * speed;
        moving = false;
        sailing = false;
    }

    public void resetBoat()
    {
        transform.position = spawnPoint.transform.position;
        rb2d.velocity = Vector2.zero;
        moving = false;
        sailing = false;
        GetComponent<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    
        if (moving)
        {
            rb2d.velocity = velocity;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            velocity = other.GetComponent<Rigidbody2D>().velocity;
            player.SetActive(false);
            moving = true;
            print("The boat sails!");
            StartCoroutine(waiter());

        }
        if (other.tag == "Island" && sailing)
        {
            moving = false;
            rb2d.velocity = Vector2.zero;
            print("The boat stops!");
            GetComponent<Collider2D>().enabled = false;
            player.transform.position = transform.position;
            player.SetActive(true);
        }
    }
    void OnBecameInvisible()
    {
        print("Boat became invisible");
        transform.position = new Vector3(-transform.position.x, -transform.position.y);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        sailing = true;
    }
}
