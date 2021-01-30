using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Mob
{
    private float distancePatrolled;
    public float distanceToPatrol = 5.0f;

    public float startDelay = 0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        distancePatrolled = 0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        distancePatrolled += Time.deltaTime * speed;
        if (distancePatrolled >= distanceToPatrol) {
            velocity *= -1;
            distancePatrolled = 0f;
        }
    }

    public override void reset()
    {
        base.reset();
        distancePatrolled = 0f;
    }

    public void startPatrol()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(startDelay);
        startMoving();
    }
}
