using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C03sk03_FinManag : SkillInterface
{
    FinManag skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new FinManag();
        skill.source = source;
        skill.level = skillLevel;
        skill.needTarget = true;
        skill.mpCost = skillLevel * 2;
        return skill;
    }
}

[System.Serializable]
public class FinManag : paSkill
{
    public override void executeAction()
    {
        if (source.MPcurr < mpCost)
        {
            message = source.name + " has not enough MP!\n";
        }
        else
        {
            source.MPcurr -= mpCost;
            int recov = 0;
            int maxSeverity = 0;
            int seSum = 0;
            foreach(StatusEffect se in target.statusEffectList)
            {
                if (se.level > maxSeverity)
                {
                    maxSeverity = se.level;
                }
                seSum++;
            }
            recov = level * maxSeverity * seSum;
            target.HealDamage(recov);
            if (seSum < 1)
            {
                message = target.name+" has no status effects!";
            }
            else
            {
                message = target.name+" has been refreshed, recovering" + recov + "HP!";
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
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }

    public override int GetMPCost(int level)
    {
        return level * 2;
    }
}
