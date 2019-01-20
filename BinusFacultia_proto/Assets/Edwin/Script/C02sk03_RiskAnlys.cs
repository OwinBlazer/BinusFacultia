using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C02sk03_RiskAnlys : SkillInterface
{
    RiskAnlys skill;
    public int duration;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new RiskAnlys();
        skill.source = source;
        skill.level = skillLevel;
        skill.duration = duration;
        skill.needTarget = true;
        skill.mpCost = mpCost+4*skillLevel;
        return skill;
    }
}

[System.Serializable]
public class RiskAnlys : paSkill
{
    private int damage;
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
            damage = CalculateDamage(source.atk, target);
            message = target.name + " is attacked for " + damage + " and inflicted SPD and ATK DOWN!\n";
            target.TakeDamage(damage);
            StatusEffect se = new Ef_AtkDOWN();
            se.InitializeSE(level, duration);
            target.InflictStatus(se);
            se = new Ef_DefDOWN();
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

    public override int GetMPCost(int level)
    {
        return mpCost + 4 * level;
    }
}
