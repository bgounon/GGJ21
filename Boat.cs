using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boat : Mob
{
    private bool sailing;
    private GameObject player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sailing = false;
    }

    public override void reset()
    {
        base.reset();
        stopMoving();
        sailing = false;
        GetComponent<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            player.GetComponent<Player>().sail();
            velocity = other.GetComponent<Rigidbody2D>().velocity;
            player.SetActive(false);
            moving = true;
            print("The boat sails!");
            sound.boatSound();
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
            player.GetComponent<Player>().stopSail();
        }
    }
    void OnBecameInvisible()
    {
        transform.position = new Vector3(-transform.position.x, -transform.position.y);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        sailing = true;
    }
}
