using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerGold : MonoBehaviour {

    public int gold;
    public int hpQty;
    public int mpQty;
    public int poisonItemQty;

    public Text goldText;
    public Text hpPotionQty;
    public Text mpPotionQty;
    public Text poisonQty;

    public Button buyPotion25;
    public Button buyPotion50;
    // Use this for initialization
    void Start () {
        gold = 200;
        goldText.text = "Gold " + gold.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (gold < 0)
            goldText.text = "Gold " + "0";
    }

    public void buyHpPotion()
    {
        if (gold == 0)
        {
            PlayerPrefs.SetInt("hpQty", hpQty);
        }
        else
        {
            hpQty += 1;
            gold -= 25;
            goldText.text = "Gold " + gold.ToString();
            PlayerPrefs.SetInt("hpQty", 99);
            hpPotionQty.text = hpQty.ToString();
        }
    }

    public void buyMpPotion()
    {
        if (gold == 0)
        {
            PlayerPrefs.SetInt("mpQty", mpQty);
        }
        else
        {
            mpQty += 1;
            gold -= 25;
            goldText.text = "Gold " + gold.ToString();
            PlayerPrefs.SetInt("mpQty", 99);
            mpPotionQty.text = mpQty.ToString();

        }
    }

    public void buyPoisonPotion()
    {
        if (gold == 0)
        {
            PlayerPrefs.SetInt("poisonQty", poisonItemQty);
        }
        else if (gold > 50)
        {
            poisonItemQty += 1;
            gold -= 50;
            goldText.text = "Gold " + gold.ToString();
            PlayerPrefs.SetInt("poisonQty", 99);
            poisonQty.text = poisonItemQty.ToString();
        }
    }
}
