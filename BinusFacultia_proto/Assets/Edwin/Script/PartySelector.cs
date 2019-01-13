using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PartySelector : MonoBehaviour {
    public PlayerLoader playerLoader;
    int chosenID = 0;
    [SerializeField] Image previewImg;
    [SerializeField] Text[] statText;
    [SerializeField] int[] activeChara = new int[3];
    [SerializeField] mainmenu sceneLoader;
    [SerializeField] Animator anim;
    [SerializeField] Button[] buttonList;
    public void Start()
    {
        for(int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.GetInt("char0"+(i+1)+"isUnlocked", 0)==1)
            {
                buttonList[i].interactable = true;
            }
            else
            {
                buttonList[i].interactable = false;
            }
        }
        
        for(int i = 0; i < 3; i++)
        {
            activeChara[i] = -1;
            buttonList[i].interactable = true;
        }
    }
    public void addToParty()
    {
        bool alreadyChosen = false;
        //index = smallest empty plot
        int index = 99;
        for (int i = 0; i < 3; i++)
        {
            if (activeChara[i] == chosenID)
            {
                alreadyChosen = true;
                activeChara[i] = -1;
                anim.SetBool("Select" + chosenID, true);
                break;
            }
            if (activeChara[i] == -1)
            {
                if (i < index)
                {
                    index = i;
                }
            }
        }
        if (!alreadyChosen&&index!=99)
        {
            activeChara[index] = chosenID;
            anim.SetBool("Select" + chosenID, false);
        }
    }
    public void SelectUnit(int ID)
    {
        chosenID = ID;
        //select change view
        PlayerChara pChara = playerLoader.allPlayerChara[ID].GetComponent<PlayerChara>();
        previewImg.sprite = pChara.chara.sprite;
        statText[0].text = pChara.chara.name;
        statText[1].text = pChara.chara.baseHPmax.ToString();
        statText[2].text = pChara.chara.baseMPmax.ToString();

        statText[3].text = pChara.chara.baseAtk.ToString();
        statText[4].text = pChara.chara.baseDef.ToString();
        statText[5].text = pChara.chara.baseSpd.ToString();

    }
    public void dispatchSquad()
    {
        //Debug.Log("dispatching Squad");
        //check if party is filled
        //load combat if yes
        bool partyFull = true;
        foreach(int i in activeChara)
        {
            if (i == -1)
            {
                partyFull = false;
            }
        }
        if (partyFull)
        {
            //save, reset session, load next scene
            for (int i = 0; i < 3; i++)
            {
                PlayerChara pchara = playerLoader.allPlayerChara[activeChara[i]].GetComponent<PlayerChara>();
                pchara.chara.Initialize();
                playerLoader.PlayerCharaSave(i, pchara );
            }
            PlayerPrefs.DeleteKey("sessionDetails");
            PlayerPrefs.DeleteKey("enemyDetails");
            //Debug.Log("Passed deletion, loading now");
            //sceneLoader.LoadSceneNamed("Combat_Test");
            sceneLoader.LoadSceneNamed("Combat");
        }
    }
}
