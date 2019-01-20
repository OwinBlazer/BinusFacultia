using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C01sk03_CyberLaw : SkillInterface
{
    CyLaw skill;
    public override PlayerAction GetSkill(Chara source, List<Chara> allChara)
    {
        skill = new CyLaw();
        skill.source = source;
        skill.level = skillLevel;
        skill.needTarget = true;
        skill.mpCost = (int)Mathf.Floor(Mathf.Pow((skillLevel + 1),1.5f));
        return skill;
    }
}

[System.Serializable]
public class CyLaw : paSkill
{
    public override void executeAction()
    {
        GameObject.FindObjectOfType<CombatEngine>().PlayBuffFX(target);
        if (source.MPcurr < mpCost)
        {
            message = source.name + " does not have enough MP!\n";
        }
        else
        {
            source.MPcurr -= mpCost;
            foreach(StatusEffect se in target.statusEffectList)
            {
                se.ResetEffect(target);
                se.level += level;
                se.RunEffect(target);
            }
            GameObject.FindObjectOfType<CombatEngine>().PlayBuffFX(target);
        }
    }

    public override void updateLog(Text targetBox)
    {
        message = source.name + " amplified " + target.name + "'s status effects!\n";
        targetBox.text += message;
    }

    public override List<Chara> GetValidTarget(List<Chara> allChara)
    {
        List<Chara> validTarget = new List<Chara>();
        foreach (Chara chara in allChara)
        {
            if (chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }

    public override int GetMPCost(int level)
    {
        return (int)Mathf.Floor(Mathf.Pow((level + 1), 1.5f));
    }
}

