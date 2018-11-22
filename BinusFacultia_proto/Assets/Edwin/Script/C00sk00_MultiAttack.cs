using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C00sk00_MultiAttack : SkillInterface {
    paSkill skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new MultiAttack();
        skill.source = source;
        skill.level = skillLevel;
        skill.needTarget = true;
        return skill;
    }
}

[System.Serializable]
public class MultiAttack : paSkill {
    private int damage;
    public override void executeAction()
    {
        damage = CalculateDamage(source.atk, target);
        for(int i = 0; i < 2+level; i++)
        {
            target.TakeDamage(damage);
        }
    }

    public override void updateLog(Text targetBox)
    {
        targetBox.text += source.name + " attacks " + target.name + " for " + damage + " damage "+ (2 + level) + " times!\n";
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
