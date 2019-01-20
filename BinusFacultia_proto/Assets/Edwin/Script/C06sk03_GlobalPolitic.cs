using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C06sk03_GlobalPolitic : SkillInterface
{
    GlobPol skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new GlobPol();
        skill.source = source;
        skill.level = skillLevel;
        skill.allChara = allChara;
        skill.needTarget = false;
        skill.mpCost = 5 * skillLevel;
        return skill;
    }
}

[System.Serializable]
public class GlobPol : paSkill
{
    public List<Chara> allChara;
    private int damage;
    public override void executeAction()
    {
        if (source.MPcurr < mpCost)
        {
            message = source.name + " does not have enough MP!\n";
        }
        else
        {
            source.MPcurr -= mpCost;
            List<Chara> tempCharaList = GetValidTarget(allChara);
            int listCount;
            int rng = 0;
            int doneHits = 0;
            //launches level+1 attacks at living enemies, stopping only when no more living enemies are left or number of hits reached.
            while(doneHits<level+1 && tempCharaList.Count > 0)
            {
                listCount = tempCharaList.Count;
                rng = Random.Range(0, listCount);
                damage = CalculateDamage(source.atk, tempCharaList[rng]);
                tempCharaList[rng].TakeDamage(damage);
                if (tempCharaList[rng].HPcurr <= 0)
                {
                    tempCharaList.RemoveAt(rng);
                }
                doneHits++;
            }

        }
    }

    public override void updateLog(Text targetBox)
    {
        message = "Enemies attacked and inflicted Atk DOWN!\n";
        targetBox.text += message;
    }

    public override List<Chara> GetValidTarget(List<Chara> allChara)
    {
        List<Chara> validTarget = new List<Chara>();
        foreach (Chara chara in allChara)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }

    public override int GetMPCost(int level)
    {
        return 5 * level;
    }
}
