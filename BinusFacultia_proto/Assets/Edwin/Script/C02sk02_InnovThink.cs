using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C02sk02_InnovThink : SkillInterface
{
    InnovThink skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new InnovThink();
        skill.source = source;
        skill.level = skillLevel;
        skill.needTarget = true;
        skill.mpCost = mpCost+3*skillLevel;
        return skill;
    }
}

[System.Serializable]
public class InnovThink : paSkill
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
            damage = CalculateDamage(source.atk+3*level, target);
            target.TakeDamage(damage);
            float rng = Random.Range(0, 1);
            if (rng < (level / (level + 2)))
            {
                //upon success gain gold
                int getGold = (int)(10 * Mathf.Pow((level), 2));
                PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold",2)+getGold);
                message = source.name+" yoinked "+getGold+" successfully!";
            }
            else
            {
                message = source.name+" dealt damage, but without getting extra gold.";
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
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }
}
