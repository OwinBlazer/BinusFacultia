﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyChara : MonoBehaviour {

    int coins;
   //[SerializeField]
    public Text coinsText;

    void start()
    {

        coinsText = GameObject.Find("Coins Text").GetComponent<Text>();
        coinsText.text = "Coins : $" + coins.ToString();
    }

    void update()
    {
        
    }

    public int Coins()
    {
        return coins;
    }

    public void modifyCoins(int _coins)
    {
        coins += _coins;
        coins = Mathf.Clamp(coins, 0, 999);
        Debug.Log(_coins);
        coinsText.text = "Coins : $" + coins.ToString();
    }
}
