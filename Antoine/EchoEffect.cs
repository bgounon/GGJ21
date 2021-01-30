using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect: MonoBehaviour
{
    public float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject echo;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if(timeBtwSpawns <= 0)
        {
            GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, 5f);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
