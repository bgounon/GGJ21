using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayerTriggerFinish(){
        print("Finished");
    }
    public static void PlayerTriggerDeath(){
        print("You are dead");
    }

}
