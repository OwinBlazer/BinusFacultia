using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C05sk04_LbrLaw : SkillInterface
{
    LbrLaw skill;
    public int duration;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new LbrLaw();
        skill.source = source;
        skill.level = skillLevel;
        skill.duration = duration;
        skill.needTarget = true;
        skill.mpCost = mpCost + 3 * skillLevel;
        return skill;
    }
}

[System.Serializable]
public class LbrLaw : paSkill
{
    private int damage;
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
            damage = CalculateDamage(source.atk + 5*level, target);
            target.TakeDamage(damage);
            StatusEffect se = new Ef_DefDOWN();
            se.InitializeSE(level, duration);
            target.InflictStatus(se);
        }
    }

    public override void updateLog(Text targetBox)
    {
        message = source.name + " flattens " + target.name + " for " + damage + "!\n";
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
