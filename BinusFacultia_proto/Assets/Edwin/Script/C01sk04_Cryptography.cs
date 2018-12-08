using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C01sk04_Cryptography : SkillInterface
{
    Crypto skill;
    public int duration;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new Crypto();
        skill.source = source;
        skill.level = skillLevel;
        skill.duration = duration;
        skill.needTarget = true;
        skill.mpCost = mpCost + 3*(skillLevel-1);
        return skill;
    }
}

[System.Serializable]
public class Crypto : paSkill
{
    public int duration;
    public override void executeAction()
    {
        if (source.MPcurr < mpCost)
        {
            message = source.name + " does not have enough MP!\n";
        }
        else
        {
            source.MPcurr -= mpCost;
            message = target.name + " is OVERLOADED!\n";
            StatusEffect se = new Ef_Poison();
            se.InitializeSE(level, duration);
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
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }
}
