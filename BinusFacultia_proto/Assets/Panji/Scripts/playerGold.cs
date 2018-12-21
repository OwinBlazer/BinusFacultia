using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerGold : MonoBehaviour {

    public int gold=0;
    public int item001Qty;
    public int item002Qty;
    public int item003Qty;
    public int item004Qty;

    public Text goldText;
    public Text Item001Qty;
    public Text Item002Qty;
    public Text Item003Qty;
    public Text Item004Qty;

    public Button buyItem500;
    public Button buyItem60;
    public Button buyItem100;
    public Button buyItem125;
    // Use this for initialization
    void Start () {
        //gold = 200;
        //PlayerPrefs.SetInt("Player Gold", 1000);
        //resetGold();
        gold = PlayerPrefs.GetInt("Player Gold");
        item001Qty = PlayerPrefs.GetInt("Item001 Qty");
        item002Qty = PlayerPrefs.GetInt("Item002 Qty");
        item003Qty = PlayerPrefs.GetInt("Item003 Qty");
        item004Qty = PlayerPrefs.GetInt("Item004 Qty");
        //text update
        Item001Qty.text = PlayerPrefs.GetInt("Item001 Text").ToString();
        Item002Qty.text = PlayerPrefs.GetInt("Item002 Text").ToString();
        Item003Qty.text = PlayerPrefs.GetInt("Item003 Text").ToString();
        Item004Qty.text = PlayerPrefs.GetInt("Item004 Text").ToString();
        //goldText.text = gold.ToString() + " G";
    }
	
	// Update is called once per frame
	void Update () {
        goldText.text = gold.ToString() + " G";
        PlayerPrefs.SetInt("Player Gold", gold);
        PlayerPrefs.SetInt("Item001 Qty", item001Qty);
        PlayerPrefs.SetInt("Item002 Qty", item002Qty);
        PlayerPrefs.SetInt("Item003 Qty", item003Qty);
        PlayerPrefs.SetInt("Item004 Qty", item004Qty);

        Item001Qty.text = item001Qty.ToString();
        Item002Qty.text = item002Qty.ToString();
        Item003Qty.text = item003Qty.ToString();
        Item004Qty.text = item004Qty.ToString();

        if (gold < 0)
            goldText.text = "0 " + "G";
    }

    public void Item001()
    {
        if (gold == 0)
        {
            //item001Qty = PlayerPrefs.GetInt("item001Qty"/*, item001Qty*/);
        }
        else if(gold >= 500)
        {
            item001Qty += 1;
            gold -= 500;
            goldText.text = gold.ToString() + " G";
            /*item001Qty = */PlayerPrefs.SetInt("item001Qty", item001Qty);
            Item001Qty.text = item001Qty.ToString();
            //item003Qty = PlayerPrefs.GetInt("item004Qty"/*, item004Qty*/);
        }
    }

    public void Item002()
    {
        if (gold == 0)
        {
            //item002Qty = PlayerPrefs.GetInt("item002Qty"/*, item002Qty*/);
        }
        else if(gold >= 60)
        {
            item002Qty += 1;
            gold -= 60;
            goldText.text = gold.ToString() + " G";
            /*item002Qty = */PlayerPrefs.SetInt("item002Qty", item002Qty);
            Item002Qty.text = item002Qty.ToString();
            //item003Qty = PlayerPrefs.GetInt("item004Qty"/*, item004Qty*/);
        }
    }

    public void Item003()
    {
        if (gold == 0)
        {
            //item003Qty = PlayerPrefs.GetInt("item003Qty"/*, item003Qty*/);
        }
        else if (gold >= 100)
        {
            item003Qty += 1;
            gold -= 100;
            goldText.text = gold.ToString() + " G";
            /*item003Qty = */PlayerPrefs.SetInt("item003Qty", item003Qty);
            Item003Qty.text = item003Qty.ToString();
            //item003Qty = PlayerPrefs.GetInt("item004Qty"/*, item004Qty*/);
        }
    }

    public void Item004()
    {
        if (gold == 0)
        {
            //item004Qty = PlayerPrefs.GetInt("item004Qty"/*, item004Qty*/);
        }
        else if (gold >= 125)
        {
            item004Qty += 1;
            gold -= 125;
            goldText.text = gold.ToString() + " G";
            /*item004Qty = */PlayerPrefs.SetInt("item004Qty", item004Qty);
            Item004Qty.text = item004Qty.ToString();
            //item004Qty = PlayerPrefs.GetInt("item004Qty"/*, item004Qty*/);
        }
    }

    void resetGold()
    {
        PlayerPrefs.DeleteKey("Item001 Qty");
        PlayerPrefs.DeleteKey("Item002 Qty");
        PlayerPrefs.DeleteKey("Item003 Qty");
        PlayerPrefs.DeleteKey("Item004 Qty");
    }
}
