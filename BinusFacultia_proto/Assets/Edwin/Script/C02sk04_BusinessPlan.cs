using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C02sk04_BusinessPlan : SkillInterface
{
    BusPlan skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new BusPlan();
        skill.source = source;
        skill.level = skillLevel;
        skill.needTarget = true;
        skill.mpCost = mpCost + 5 * skillLevel;
        return skill;
    }
}

[System.Serializable]
public class BusPlan : paSkill
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
            message = target.name + " transferred MP and amplified!\n";
            target.HealMP(5 * level);
            foreach (StatusEffect se in target.statusEffectList)
            {
                se.ResetEffect(target);
                se.level += level;
                se.RunEffect(target);
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
}
