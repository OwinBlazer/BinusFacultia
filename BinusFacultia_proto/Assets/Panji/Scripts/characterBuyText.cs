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
    [SerializeField] AudioClip buySound;
    private int[] characterCost = new int[] { 0, 0, 0, 1000, 1000, 1000 };
    private int selectCharacterIndex;
    private int selectedCharPref;
    public void SelectedCharPrefIndex(int index)
    {
        selectedCharPref = index;
    }

    // Use this for initialization
    void Start () {
        //points = 5000;
        //pointsText.text = points.ToString()+" Pts";
        //saveManager.Instance.state.points = 5000;
        PlayerPrefs.SetInt("point", saveManager.Instance.state.points);
        //PlayerPrefs.GetInt("Player Points");
        updatePointsText();
        initShop();
	}
	
	// Update is called once per frame
	void Update () {
        //pointsText.text = points.ToString() + " Pts";
        //PlayerPrefs.SetInt("Player Points", saveManager.Instance.state.points);
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
        pointsText.text = saveManager.Instance.state.points.ToString() + " Pts";
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
           characterBuySetText.text = "Buy\n" +characterCost[currentIndex].ToString() + " Pts";
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
                PlayerPrefs.SetInt("char0" + (selectedCharPref+1)+ "isUnlocked", 1);
                Debug.Log(PlayerPrefs.GetInt("char0" + (selectedCharPref + 1) + "isUnlocked",0));
                AudioHandler.audioHandler.playSFX(buySound);
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
