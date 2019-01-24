using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour {
    //handles player loading
    //list of used characters
    //saves current party status
    //references to all character prefabs, so can spawn needed ones
    public List<GameObject> allPlayerChara;
    public List<ItemInterface> allItem;
    [HideInInspector]
    public List<PlayerChara> party;

    //temporary testing
    public void initializeParty()
    {
        for(int i = 0; i < 3; i++)
        {
            // tempchara holds the chosen character temporarily
            //CHANGE THE i@<========================================================
            party.Add(transform.GetChild(i).GetComponent<PlayerChara>());
            PlayerDataLoad(i);
        }
        ItemDataLoad();
    }
    void ItemDataLoad()
    {
        for(int i = 0;i< 4; i++)
        {
            int loadedQty = PlayerPrefs.GetInt("item0"+(i+1),0);
            allItem[i].changeQtyBy(loadedQty - allItem[i].GetQty());
        }
    }
    public void SaveParty()
    {
        for(int i = 0; i < 3; i++)
        {
            PlayerCharaSave(i,party[i]);
        }
    }
    public void PlayerCharaSave(int ID,PlayerChara pChara)
    {
        string saveData="";
        //store starts with id
        saveData += GetIDOf(pChara.chara.name)+"#"; //index 0

        //base stats and name and skill type will be pulled from id

        //saving current active stats
        saveData += pChara.chara.HPmax + "#";
        saveData += pChara.chara.MPmax + "#";
        saveData += pChara.chara.atk + "#";
        saveData += pChara.chara.def + "#";
        saveData += pChara.chara.spd + "#"; //index 5

        //saving battle stats
        saveData += pChara.chara.HPcurr + "#";
        saveData += pChara.chara.MPcurr + "#";

        saveData += pChara.chara.efShield + "#";
        saveData += pChara.chara.efTauntRate + "#";
        saveData += pChara.chara.efStunRate + "#"; //index 10
        saveData += pChara.chara.efRecov + "#";
        saveData += pChara.chara.efExtend + "#";
        saveData += pChara.chara.efPoison + "#";

        saveData += pChara.chara.actionPointMax + "#";

        int seIndex=0;
        //saving status effects
        foreach(StatusEffect se in pChara.chara.statusEffectList)
        {
            saveData += se.StatusID +"^";
            saveData += se.level + "^";
            saveData += se.duration;
            if (seIndex+1 < pChara.chara.statusEffectList.Count)
            {
                saveData += "&";
                
            }
            seIndex++;
        }
        saveData += "#"; //index 15
        //saving allocation stats
        saveData += pChara.skillList[0].skillLevel + "#";
        saveData += pChara.skillList[1].skillLevel + "#";
        saveData += pChara.skillList[2].skillLevel + "#";
        saveData += pChara.skillList[3].skillLevel + "#";

        saveData += pChara.trainCount+"#"; //index 20
        saveData += pChara.spCount; //index 21

        PlayerPrefs.SetString("partyDetails"+ID,saveData);
    }
    public PlayerChara PlayerDataLoad(int ID)
    {
        PlayerChara pChara = party[ID].GetComponent<PlayerChara>();
        pChara.chara = new Chara("", 0, 0, 0, 0, 0, false);
        PlayerChara tempCharaHolder;
        string[] saveData;
        saveData = PlayerPrefs.GetString("partyDetails"+ID,"").Split('#');
        if (saveData.Length < 18)
        {
            pChara.chara.name = "ERRORNOTFOUND";
            return pChara;
        }
        else
        {
            tempCharaHolder = allPlayerChara[int.Parse(saveData[0])].GetComponent<PlayerChara>();
            //set return pChara based on ID(bases)
            pChara.chara.sprite = tempCharaHolder.chara.sprite;
            pChara.chara.name = tempCharaHolder.chara.name;
            pChara.chara.baseHPmax = tempCharaHolder.chara.baseHPmax;
            pChara.chara.baseMPmax = tempCharaHolder.chara.baseMPmax;
            pChara.chara.baseAtk = tempCharaHolder.chara.baseAtk;
            pChara.chara.baseDef = tempCharaHolder.chara.baseDef;
            pChara.chara.baseSpd = tempCharaHolder.chara.baseSpd;
            pChara.skillList = tempCharaHolder.skillList;

            //set current active stats
            pChara.chara.HPmax = int.Parse(saveData[1]);
            pChara.chara.MPmax = int.Parse(saveData[2]);
            pChara.chara.atk = int.Parse(saveData[3]);
            pChara.chara.def = int.Parse(saveData[4]);
            pChara.chara.spd = int.Parse(saveData[5]);

            //set battle stats
            pChara.chara.HPcurr = int.Parse(saveData[6]);
            pChara.chara.MPcurr = int.Parse(saveData[7]);

            pChara.chara.efShield = int.Parse(saveData[8]);
            pChara.chara.efTauntRate = int.Parse(saveData[9]);
            pChara.chara.efStunRate = int.Parse(saveData[10]);
            pChara.chara.efRecov = int.Parse(saveData[11]);
            pChara.chara.efExtend = int.Parse(saveData[12]);
            pChara.chara.efPoison = int.Parse(saveData[13]);

            pChara.chara.actionPointMax = int.Parse(saveData[14]);
            //set status effects
            if (saveData[15].Length > 0)
            {
                string[] seData = saveData[15].Split('&');
                foreach (string seSet in seData)
                {
                    string[] seDetail = seSet.Split('^');
                    /*1 = AtkUP
                        2 = SpdUP
                        3 = DefUP
                        4 = AtkDOWN
                        5 = SpdDOWN
                        6 = DefDOWN
                        7 = HPDOwn
                        11 = Poison(Overload)
                        12 = Shield
                        13 = Extend
                        14 = Regen
                        15 = Stun
                        16 = Taunt*/
                    StatusEffect tempSE;
                    //set SE according to ID
                    switch (int.Parse(seDetail[0]))
                    {
                        case 1:
                            tempSE = new Ef_AtkUP();
                            break;
                        case 2:
                            tempSE = new Ef_SpdUP();
                            break;
                        case 3:
                            tempSE = new Ef_DefUP();
                            break;
                        case 4:
                            tempSE = new Ef_AtkDOWN();
                            break;
                        case 5:
                            tempSE = new Ef_SpdDOWN();
                            break;
                        case 6:
                            tempSE = new Ef_DefDOWN();
                            break;
                        case 7:
                            tempSE = new Ef_HPDOWN();
                            break;
                        case 11:
                            tempSE = new Ef_Poison();
                            break;
                        case 12:
                            tempSE = new Ef_Shield();
                            break;
                        case 13:
                            tempSE = new Ef_Extend();
                            break;
                        case 14:
                            tempSE = new Ef_Regen();
                            break;
                        case 15:
                            tempSE = new Ef_Stun();
                            break;
                        case 16:
                            tempSE = new Ef_Taunt();
                            break;
                        default:
                            //in case buff not found, turn to regen
                            tempSE = new Ef_Regen();
                            break;
                    }
                    tempSE.InitializeSE(int.Parse(seDetail[1]), int.Parse(seDetail[2]));
                    pChara.chara.statusEffectList.Add(tempSE);
                }
            }
            

            //allocation
            pChara.skillList[0].skillLevel = int.Parse(saveData[16]);
            pChara.skillList[1].skillLevel = int.Parse(saveData[17]);
            pChara.skillList[2].skillLevel = int.Parse(saveData[18]);
            pChara.skillList[3].skillLevel = int.Parse(saveData[19]);

            pChara.trainCount = int.Parse(saveData[20]);
            pChara.spCount = int.Parse(saveData[21]);
            return pChara;
        }
    }
    private int GetIDOf(string name)
    {
        int ID = -1;
        int tempIndex = 0;
        while (ID == -1)
        {
            PlayerChara pChara = allPlayerChara[tempIndex].GetComponent<PlayerChara>();
            
            if (pChara.chara.name==name)
            {
                ID = tempIndex;
                //Debug.Log("id changed into "+tempIndex);
            }
            tempIndex++;
        }
        //Debug.Log("exited");
        return ID;
    }
    public List<Chara> GetPlayerCharacters()
    {
        List<Chara> tempCharaList = new List<Chara>();
        foreach(PlayerChara pChara in party)
        {
            tempCharaList.Add(pChara.chara);
        }
        return tempCharaList;
    }
    public List<PlayerChara> GetParty()
    {
        List<PlayerChara> tempCharaList = new List<PlayerChara>();
        foreach (PlayerChara pChara in party)
        {
            tempCharaList.Add(pChara);
        }
        return tempCharaList;
    }
    public List<SkillInterface> GetSkills(Chara chara)
    {
        List<SkillInterface> tempSkillList = new List<SkillInterface>();
        foreach(PlayerChara pChara in party)
        {
            if(pChara.chara.name == chara.name)
            {
                tempSkillList = pChara.skillList;
            }
        }
        return tempSkillList;
    }
    public List<ItemInterface> GetItems()
    {
        return allItem;
    }
}
