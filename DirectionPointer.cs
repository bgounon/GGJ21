using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{
    public float angularSpeed = 60f;
    private bool rotating;

    // Start is called before the first frame update
    void Start()
    {
        rotating = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating) {
            transform.Rotate(0,0,angularSpeed*Time.deltaTime); //rotates 50 degrees per second around z axis
        }
    }

    public void stopRotating() {
        rotating = false;
    }

    public void startRotating() {
        rotating = true;
    }

}
