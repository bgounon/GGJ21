using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject destination;
    private Sound sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = FindObjectOfType<Sound>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            sound.holeSound();
            other.transform.position = destination.transform.position;
            Collider2D destinationCollider = destination.GetComponent<Collider2D>();
            destinationCollider.enabled = false;
            StartCoroutine(waiter(destinationCollider));
        }
    }

    IEnumerator waiter(Collider2D destinationCollider)
    {
        yield return new WaitForSeconds(1);
        destinationCollider.enabled = true;
    }
}
