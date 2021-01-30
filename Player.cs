using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEditor.CrashReporting;
using UnityEngine;

public class Player : Mob
{
    private bool sailing;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sailing = false;
    }

    public override void reset()
    {
        sailing = false;
        base.reset();
    }

    public void sail() {
        sailing = true;
    }

    public void stopSail() {
        sailing = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Finish") {
            manager.PlayerTriggerFinish();
        }
        if (col.gameObject.tag == "Death") {
            manager.PlayerTriggerDeath();
        }
        if (col.gameObject.tag == "Coin") {
            Coin coin = col.gameObject.GetComponent<Coin>();
            manager.addScore(coin.value);
            Destroy(coin.gameObject);
        }

    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Island" && sailing == false && !rb2d.isKinematic) {
            manager.PlayerTriggerDrown();
        }

    }

}
