using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect: MonoBehaviour
{
    public float timeBtwSpawns = 1f;

    public GameObject trail;
    public GameObject player;
    public GameObject boat;
    private Vector2? direction;
    private bool trailing;
    private bool isWaiting = false;
    private List<GameObject> traillist;

    private void Start()
    {
        traillist = new List<GameObject>();
    }

    void Update()
    {
        if (trailing && !isWaiting)
        {

            GameObject instance;
            if (player.active)
            {
                assignDirection(player.GetComponent<Rigidbody2D>().velocity);
                instance = (GameObject)Instantiate(trail, player.transform.position, Quaternion.identity);
            } else
            {
                assignDirection(boat.GetComponent<Rigidbody2D>().velocity);
                instance = (GameObject)Instantiate(trail, boat.transform.position, Quaternion.identity);
            }
            
            
            instance.transform.right = direction.Value;
            direction = null;
            traillist.Add(instance);
            StartCoroutine(waiter());
        }
    }

    private void assignDirection(Vector2 direction)
    {
        if(this.direction == null)
        {
            this.direction = direction;
        }
    }

    public void startTrail(Vector2 direction)
    {
        trailing = true;
        this.direction = direction;
    }
    public void stopTrail()
    {
        trailing = false;
        this.direction = null;

    }

    public void clearTrail()
    {
        stopTrail();
        traillist.ForEach(item => Destroy(item));
        traillist.Clear();
    }

    IEnumerator waiter()
    {
        isWaiting = true;
        yield return new WaitForSeconds(timeBtwSpawns);
        isWaiting = false;
    }
}
