using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C03sk04_CulArt : SkillInterface
{
    CulArt skill;
    public int duration;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new CulArt();
        skill.source = source;
        skill.level = skillLevel;
        skill.duration = duration;
        skill.needTarget = true;
        skill.mpCost = mpCost + skillLevel * 3;
        return skill;
    }
}

[System.Serializable]
public class CulArt : paSkill
{
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
            message = target.name+"'s gains greater Atk UP!\n";
            StatusEffect se = new Ef_AtkUP();
            se.InitializeSE(level+2, duration);
            target.InflictStatus(se);
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
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }
}
