using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect: MonoBehaviour
{
    public float timeBtwSpawns = 1f;

    public GameObject trail;
    private Player player;
    private Vector2 direction;
    private bool trailing;
    private bool isWaiting = false;
    private List<GameObject> traillist;

    private void Start()
    {
        player = GetComponent<Player>();
        traillist = new List<GameObject>();
    }

    void Update()
    {
        if (trailing && !isWaiting)
        {
            direction = player.getDirection();
            GameObject instance = (GameObject)Instantiate(trail, player.transform.position, Quaternion.identity);
            instance.transform.right = direction;
            traillist.Add(instance);
            StartCoroutine(waiter());
        }
    }

    public void startTrail()
    {
        trailing = true;
    }
    public void stopTrail()
    {
        trailing = false;

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
