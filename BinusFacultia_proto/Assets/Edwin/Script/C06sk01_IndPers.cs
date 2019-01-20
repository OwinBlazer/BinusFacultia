using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C06sk01_IndPers : SkillInterface
{
    IndPers skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new IndPers();
        skill.source = source;
        skill.level = skillLevel;
        skill.allChara = allChara;
        skill.needTarget = false;
        skill.mpCost = mpCost + 3 * skillLevel;
        return skill;
    }
}

[System.Serializable]
public class IndPers : paSkill
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
            foreach(Chara chara in allChara)
            {
                if (chara.isEnemy && chara.HPcurr > 0)
                {
                    damage = CalculateDamage(source.atk + (int)Mathf.Pow(level + 1, 2), chara);
                    chara.TakeDamage(damage);
                }
            }
        }
    }

    public override void updateLog(Text targetBox)
    {
        message = "Enemies are sprayed with Perspective!\n";
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
        return mpCost + 3 * level;
    }
}
