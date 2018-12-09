using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C06sk02_IntRelation : SkillInterface
{
    IntRel skill;
    public int duration;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new IntRel();
        skill.source = source;
        skill.level = skillLevel;
        skill.allChara = allChara;
        skill.duration = duration;
        skill.needTarget = false;
        skill.mpCost = mpCost + 2 * skillLevel;
        return skill;
    }
}

[System.Serializable]
public class IntRel : paSkill
{
    public List<Chara> allChara;
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
            StatusEffect se = new Ef_AtkDOWN();
            se.InitializeSE(level, duration);
            foreach (Chara chara in allChara)
            {
                if (chara.isEnemy && chara.HPcurr > 0)
                {
                    damage = CalculateDamage(source.atk , target);
                    target.TakeDamage(damage);
                    target.InflictStatus(se);
                }
            }
        }
    }

    public override void updateLog(Text targetBox)
    {
        message = "Enemies attacked and inflicted Atk DOWN!\n";
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
