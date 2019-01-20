using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SEDetailViewHandler : MonoBehaviour {
    [SerializeField]List<CharSEdetail> seDetailList = new List<CharSEdetail>();
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite[] statusIcon;
    [SerializeField] Animator anim;
	// Use this for initialization
	void Start () {
        //              Panel               Each Chara              0=name. 1=ListMsk->0=layout->0-13=seDetailGroup->1=numer level, 3=numer-Duration, 4=image
        //Text name = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        Transform panelParent = transform.GetChild(0);
        for(int i = 0; i < panelParent.childCount; i++)
        {
            CharSEdetail charDetail = new CharSEdetail();
            charDetail.seDetail = new List<SeDetail>();
            charDetail.nameText = panelParent.GetChild(i).GetChild(0).GetComponent<Text>();
            Transform SEDetailParent = panelParent.GetChild(i).GetChild(1).GetChild(0);
            for(int j = 0; j < SEDetailParent.childCount; j++)
            {
                SeDetail seDetail = new SeDetail();
                //level
                seDetail.levelText = SEDetailParent.GetChild(j).GetChild(0).GetComponent<Text>();
                //duration
                seDetail.durationText = SEDetailParent.GetChild(j).GetChild(1).GetComponent<Text>();
                //icon
                seDetail.icon =
                    SEDetailParent.
                    GetChild(j).GetChild(2).
                    GetComponent<Image>();
                charDetail.seDetail.Add(seDetail);
            }
            seDetailList.Add(charDetail);
        }
	}
    private Sprite GetIconOfSEID(int StatusID)
    {
        switch (StatusID) {
            case 1: return statusIcon[0];
                //tempSE = new Ef_AtkUP();
            case 2:
                return statusIcon[1];
                //tempSE = new Ef_SpdUP();
            case 3:
                return statusIcon[2];
                //tempSE = new Ef_DefUP();
            case 4:
                return statusIcon[3];
                //tempSE = new Ef_AtkDOWN();
            case 5:
                return statusIcon[4];
                //tempSE = new Ef_SpdDOWN();
            case 6:
                return statusIcon[5];
                //tempSE = new Ef_DefDOWN();
            case 7:
                return statusIcon[6];
                //tempSE = new Ef_HPDOWN();
            case 11:
                return statusIcon[7];
                //tempSE = new Ef_Poison();
            case 12:
                return statusIcon[8];
                //tempSE = new Ef_Shield();
            case 13:
                return statusIcon[9];
                //tempSE = new Ef_Extend();
            case 14:
                return statusIcon[10];
                //tempSE = new Ef_Regen();
            case 15:
                return statusIcon[11];
                //tempSE = new Ef_Stun();
            case 16:
                return statusIcon[12];
                //tempSE = new Ef_Taunt();
            default: return emptySprite;
        }

        
    }
    public void updateSEList(List<Chara> allChara)
    {
        for(int i = 0; i < 8; i++)
        {
            seDetailList[i].nameText.text = allChara[i].name;
            if (allChara[i].HPcurr <= 0)
            {
                seDetailList[i].nameText.text = "";
            }
            for (int j = 0; j < seDetailList[i].seDetail.Count; j++)
            {
                seDetailList[i].seDetail[j].levelText.text = "";
                seDetailList[i].seDetail[j].durationText.text = "";
                seDetailList[i].seDetail[j].icon.sprite = emptySprite;
            }
            for (int j = 0; j < allChara[i].statusEffectList.Count; j++)
            {
                seDetailList[i].seDetail[j].levelText.text = "lv."+allChara[i].statusEffectList[j].level.ToString();
                seDetailList[i].seDetail[j].durationText.text = allChara[i].statusEffectList[j].duration.ToString()+" turns";
                seDetailList[i].seDetail[j].icon.sprite = GetIconOfSEID( allChara[i].statusEffectList[j].StatusID);
            }
        }
        /*
        for(int i = 0; i < allChara.Count; i++)
        {
            for (int j = 0; j < seDetailList[i].seDetail.Count; j++)
            {
                seDetailList[i].seDetail[j].levelText.text = "";
                seDetailList[i].seDetail[j].durationText.text = "";
                seDetailList[i].seDetail[j].icon.sprite = emptySprite;

            }
            if (allChara[i].name != "")
            {
                seDetailList[i].nameText.text = allChara[i].name;
                
                if (allChara[i].statusEffectList.Count > 0)
                {
                    Debug.Log(allChara[i] + " with status effec count " + allChara[i].statusEffectList.Count);
                    for (int j = 0; j < allChara[i].statusEffectList.Count; i++)
                    {
                        seDetailList[i].seDetail[j].levelText.text = "lv." + allChara[i].statusEffectList[j].level.ToString();
                        seDetailList[i].seDetail[j].durationText.text = allChara[i].statusEffectList[j].duration.ToString() + " turns";
                        seDetailList[i].seDetail[j].icon.sprite = GetIconOfSEID(allChara[i].statusEffectList[j].StatusID);
                    }
                }
                 
                
                 
            }
            else
            {
                seDetailList[i].nameText.text = "";
                for (int j = 0; j < seDetailList[i].seDetail.Count; j++)
                {
                    seDetailList[i].seDetail[j].levelText.text = "";
                    seDetailList[i].seDetail[j].durationText.text = "";
                    seDetailList[i].seDetail[j].icon.sprite = emptySprite;

                }
            }
        }
        */
    }
    public void ViewSEDetail(bool mode)
    {
        if (mode)
        {
            //open SE Details view
            anim.SetBool("SEDetailsView",true);
        }
        else
        {
            //close SE Details view
            anim.SetBool("SEDetailsView", false);
        }
    }
}
[System.Serializable]
class SeDetail {
    public Image icon;
    public Text levelText;
    public Text durationText;
}
[System.Serializable]
class CharSEdetail
{
    public Text nameText;
    public List<SeDetail> seDetail;
}
