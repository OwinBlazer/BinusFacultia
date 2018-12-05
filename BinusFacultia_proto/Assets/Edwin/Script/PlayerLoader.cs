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
        party[2].chara.HPcurr -= 5;
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
