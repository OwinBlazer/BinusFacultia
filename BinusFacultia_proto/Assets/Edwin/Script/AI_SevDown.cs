using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_SevDown : ActionInterface
{
    public int level;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_SevDown act = new eAI_SevDown(source, allChara);
        act.level = level;
        return act;
    }
}
public class eAI_SevDown : Action
{
    public int level;
    private Chara target;
    public eAI_SevDown(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " decreases " + target.name + "'s severity!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
        }
        target = viableTargets[Random.Range(0, viableTargets.Count)];
        GameObject.FindObjectOfType<CombatEngine>().PlayDebuffFX(target);
        int indexFlag = 0;
        StatusEffect se;
        while (indexFlag < target.statusEffectList.Count)
        {
            se = target.statusEffectList[indexFlag];
            se.ResetEffect(target);
            se.level -= level;
            if (se.level > 0)
            {
                se.RunEffect(target);
                indexFlag++;
            }
            else
            {
                target.statusEffectList.RemoveAt(indexFlag);
            }
        }
    }
}
