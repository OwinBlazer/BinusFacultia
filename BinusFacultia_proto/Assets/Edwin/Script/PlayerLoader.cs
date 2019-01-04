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
            //CHANGE THE 0@<========================================================
            Chara tempChara = allPlayerChara[i].GetComponent<PlayerChara>().chara;
            party.Add(transform.GetChild(i).GetComponent<PlayerChara>());
            party[i].chara = new Chara(tempChara.name, tempChara.HPmax, tempChara.MPmax, tempChara.baseAtk, tempChara.baseDef, tempChara.baseSpd, false);
            party[i].skillList = allPlayerChara[i].GetComponent<PlayerChara>().skillList;
        }

    }
    public void PlayerCharaSave(int ID,PlayerChara pChara)
    {
        string saveData="";
        //store starts with id
        saveData += GetIDOf(pChara.chara.name)+"#";

        //base stats and name and skill type will be pulled from id

        //saving current active stats
        saveData += pChara.chara.HPmax + "#";
        saveData += pChara.chara.MPmax + "#";
        saveData += pChara.chara.atk + "#";
        saveData += pChara.chara.def + "#";
        saveData += pChara.chara.spd + "#";

        //saving battle stats
        saveData += pChara.chara.HPcurr + "#";
        saveData += pChara.chara.MPcurr + "#";

        saveData += pChara.chara.efShield + "#";
        saveData += pChara.chara.efTauntRate + "#";
        saveData += pChara.chara.efStunRate + "#";
        saveData += pChara.chara.efRecov + "#";
        saveData += pChara.chara.efExtend + "#";
        saveData += pChara.chara.efPoison + "#";

        saveData += pChara.chara.actionPointMax + "#";

        //saving allocation stats
        saveData += pChara.skillList[0].skillLevel + "#";
        saveData += pChara.skillList[1].skillLevel + "#";
        saveData += pChara.skillList[2].skillLevel + "#";
        saveData += pChara.skillList[3].skillLevel + "#";

        saveData += pChara.trainCount;

        PlayerPrefs.SetString("partyDetails"+ID,saveData);
    }
    public PlayerChara PlayerDataLoad(int ID)
    {
        PlayerChara pChara = new PlayerChara();
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
            pChara.chara.name = tempCharaHolder.chara.name;
            pChara.chara.baseHPmax = tempCharaHolder.chara.baseHPmax;
            pChara.chara.baseMPmax = tempCharaHolder.chara.baseHPmax;
            pChara.chara.baseAtk = tempCharaHolder.chara.baseHPmax;
            pChara.chara.baseDef = tempCharaHolder.chara.baseHPmax;
            pChara.chara.baseSpd = tempCharaHolder.chara.baseHPmax;
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

            pChara.chara.actionPointMax = int.Parse(saveData[13]);

            //allocation
            pChara.skillList[0].skillLevel = int.Parse(saveData[14]);
            pChara.skillList[1].skillLevel = int.Parse(saveData[15]);
            pChara.skillList[2].skillLevel = int.Parse(saveData[16]);
            pChara.skillList[3].skillLevel = int.Parse(saveData[17]);

            pChara.trainCount = int.Parse(saveData[18]);
            return pChara;
        }
    }
    private int GetIDOf(string name)
    {
        int ID = 0;
        int tempIndex = 0;
        while (ID == 0)
        {
            if (allPlayerChara[tempIndex].GetComponent<PlayerChara>().chara.name.Equals(name))
            {
                ID = tempIndex;
            }
        }
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
