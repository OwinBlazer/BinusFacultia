using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonInteraction : MonoBehaviour {

    buyChara bc;


	// Use this for initialization
	void Start () {
        bc = GetComponent<buyChara>();
	}

    public void increase500()
    {
        bc.modifyCoins(500);
    }

    public void buy10()
    {
        bc.modifyCoins(-10);
    }

    public void buy15()
    {
        bc.modifyCoins(-15);
    }

    public void buy20()
    {
        bc.modifyCoins(-20);
    }

    public void buy25()
    {
        bc.modifyCoins(-25);
    }

    public void buy30()
    {
        bc.modifyCoins(-30);
    }

    public void buy35()
    {
        bc.modifyCoins(-35);
    }

    public void buy40()
    {
        bc.modifyCoins(-40);
    }

    public void buy45()
    {
        bc.modifyCoins(-45);
    }

    public void buy50()
    {
        bc.modifyCoins(-50);
    }

    public void buy55()
    {
        bc.modifyCoins(-55);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
