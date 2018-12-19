using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterBuyText : MonoBehaviour {

    public Transform characterPanel;
    public Text characterBuySetText;
    public int points;
    public Text pointsText;

    private int[] characterCost = new int[] { 1000, 1000, 1000, 1000, 1000, 1000 };
    private int selectCharacterIndex;

	// Use this for initialization
	void Start () {
        //points = 5000;
        //pointsText.text = points.ToString()+" Pts";
        //saveManager.Instance.state.points = 5000;
        PlayerPrefs.SetInt("Player Points", saveManager.Instance.state.points);
        updatePointsText();
        initShop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initShop()
    {
        if(characterPanel == null)
            Debug.Log("You didn't assign the character panel in the inspector");
        
        int i = 0;
        foreach (Transform t in characterPanel)
        {
            int currentIndex = i;

            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnCharacterSelect(currentIndex));

            i++;
        }

        i = 0;
    }

    private void setCharacter(int index)
    {
        characterBuySetText.text = "Already Have";
    }

    private void updatePointsText()
    {
        pointsText.text = saveManager.Instance.state.points.ToString();
    }

    private void OnCharacterSelect(int currentIndex)
    {
        //Debug.Log("Selecting Character Button  : " + currentIndex);
        selectCharacterIndex = currentIndex;

       if (saveManager.Instance.IsCharacterOwned(currentIndex))
       {
            
            characterBuySetText.text = "Already Have";
       }
       else
       {
           characterBuySetText.text = "Buy" /*+characterCost[currentIndex].ToString()*/;
       }
    }

    public void OnCharacterBuySet()
    {
        //Debug.Log("Buy/Set Character");

        if (saveManager.Instance.IsCharacterOwned(selectCharacterIndex))
        {
            setCharacter(selectCharacterIndex);
        }
        else
        {
            if (saveManager.Instance.BuyCharacter(selectCharacterIndex, characterCost[selectCharacterIndex]))
            {
                setCharacter(selectCharacterIndex);
                updatePointsText();
            }
            else
            {
                pointsText.text = "Not enough points!";
                Debug.Log("Not enough points!");
            }
        }
    }
}
