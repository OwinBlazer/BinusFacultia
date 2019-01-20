using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEnhancementHandler : MonoBehaviour {
    [SerializeField]PlayerLoader pl;
    [SerializeField] Text[] spText;
    [SerializeField] Text[] textBefore;
    [SerializeField] Text[] textAfter;

    [SerializeField] Image[] skillIcon;
    [SerializeField] Image[] charIcon;
    List<PlayerChara> party;
    private int chosenIndex;
    private Chara passingChara;
    private List<Chara> passingCharaList;
    // Use this for initialization
    void Start () {
        pl.initializeParty();
        party = pl.GetParty();
        for(int i = 0; i < 3; i++)
        {
            spText[i].text = "SP: "+party[i].spCount.ToString();
            charIcon[i].sprite = party[i].chara.sprite;
        }
        ViewSkillDetails(0);
	}
	public void ViewSkillDetails(int index)
    {
        chosenIndex = index;
        

        //List<SkillInterface> afterSkill;
        paSkill[] afterSkill= new paSkill[4];
        for(int i = 0; i < 4; i++)
        {
            afterSkill[i] = (paSkill)party[index].skillList[i].GetSkill(passingChara, passingCharaList);
        }

        //set afterskill, to get mp cost
        for (int i = 0; i < 4; i++)
        {
            skillIcon[i].sprite = party[index].skillList[i].skillIcon;
            afterSkill[i].level += 1;
            paSkill tempPASkill = (paSkill)party[index].skillList[i].GetSkill(passingChara, passingCharaList);
            textBefore[i].text = "Mp Cost: " + tempPASkill.GetMPCost(tempPASkill.level);
            textAfter[i].text = "MP Cost: " + tempPASkill.GetMPCost(tempPASkill.level+1);
            //set details to mp cost
            //paSkill tempPASkill = (paSkill)party[index].skillList[i].GetSkill(passingChara, passingCharaList);
            //textBefore[i].text = "Mp Cost: "+tempPASkill.mpCost.ToString();
            //tempPASkill = (paSkill)afterSkill[i].GetSkill(passingChara, passingCharaList);
            //textAfter[i].text = "Mp Cost: " +tempPASkill.mpCost.ToString();
        }
    }
	
}
