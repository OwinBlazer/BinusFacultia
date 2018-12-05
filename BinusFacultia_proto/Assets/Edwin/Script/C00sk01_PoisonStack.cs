using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C00sk01_PoisonStack : SkillInterface
{
    PoisonStack skill;
    public int duration;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new PoisonStack();
        skill.source = source;
        skill.level = skillLevel;
        skill.duration = duration;
        skill.needTarget = true;
        return skill;
    }
}

[System.Serializable]
public class PoisonStack : paSkill
{
    private int damage;
    public int duration;
    public override void executeAction()
    {
        damage = CalculateDamage(source.atk, target);
        StatusEffect se = new Ef_Poison();
        se.InitializeSE(level, duration);
        target.TakeDamage(damage);
        target.InflictStatus(se);
    }

    public override void updateLog(Text targetBox)
    {
        targetBox.text += target.name+" is attacked for "+ damage+ " and OVERLOADED!\n";
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
