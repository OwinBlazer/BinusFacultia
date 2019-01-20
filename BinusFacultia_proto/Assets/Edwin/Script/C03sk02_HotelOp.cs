using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C03sk02_HotelOp : SkillInterface
{
    HotelOp skill;
    public int duration;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new HotelOp();
        skill.source = source;
        skill.level = skillLevel;
        skill.duration = duration;
        skill.needTarget = false;
        skill.allChara = allChara;
        skill.mpCost = mpCost + skillLevel * 3;
        return skill;
    }
}

[System.Serializable]
public class HotelOp : paSkill
{
    public List<Chara> allChara;
    public int duration;
    public override void executeAction()
    {
        if (source.MPcurr < mpCost)
        {
            message = source.name + " has not enough MP!\n";
        }
        else
        {
            source.MPcurr -= mpCost;
            message = "Party is inflicted with Def UP!\n";
            foreach(Chara chara in allChara)
            {
                if (!chara.isEnemy && chara.HPcurr > 0)
                {
                    StatusEffect se = new Ef_DefUP();
                    se.InitializeSE(level, duration);
                    chara.InflictStatus(se);
                }
            }
        }

    }

    public override void updateLog(Text targetBox)
    {

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
        return mpCost + level * 3;
    }
}
