﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C05sk02_InvLaw : SkillInterface
{
    InvLaw skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new InvLaw();
        skill.source = source;
        skill.level = skillLevel;
        skill.needTarget = true;
        skill.mpCost = mpCost + 2 * skillLevel;
        return skill;
    }
}

[System.Serializable]
public class InvLaw : paSkill
{
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
            damage = CalculateDamage(source.atk + (int)Mathf.Pow(source.statusEffectList.Count+level, 2), target);
            target.TakeDamage(damage);
        }
    }

    public override void updateLog(Text targetBox)
    {
        message = source.name + " crushes " + target.name + " for " + damage + "!\n";
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
        return mpCost + 2 * level;
    }
}
