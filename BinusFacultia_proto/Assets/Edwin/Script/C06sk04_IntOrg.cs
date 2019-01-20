using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C06sk04_IntOrg : SkillInterface
{
    IntOrg skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new IntOrg();
        skill.source = source;
        skill.level = skillLevel;
        skill.needTarget = true;
        skill.mpCost = mpCost + 5*skillLevel;
        return skill;
    }
}

[System.Serializable]
public class IntOrg : paSkill
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
            damage = CalculateDamage(source.atk, target);
            target.TakeDamage(damage);
            GameObject.FindObjectOfType<CombatEngine>().PlayBuffFX(target);
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
        message = source.name + " amplified " + target.name + "'s status effects while dealing "+damage+"!\n";
        targetBox.text += message;
    }

    public override List<Chara> GetValidTarget(List<Chara> allChara)
    {
        List<Chara> validTarget = new List<Chara>();
        foreach (Chara chara in allChara)
        {
            if (chara.isEnemy&&chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }

    public override int GetMPCost(int level)
    {
        return mpCost + 5 * level;
    }
}

