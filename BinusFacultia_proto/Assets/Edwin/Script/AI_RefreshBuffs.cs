using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_RefreshBuffs : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_RefreshBuffs(source, allChara);

    }
}
public class eAI_RefreshBuffs : Action
{
    private Chara target;
    public eAI_RefreshBuffs(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " removes all BUFFS from "+target.name+"!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> validTargets = new List<Chara>();
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                validTargets.Add(chara);
            }
        }
        target = validTargets[Random.Range(0,validTargets.Count)];
        int tempIndex = 0;
        while (tempIndex < target.statusEffectList.Count)
        {
            if (target.statusEffectList[tempIndex].isBuff)
            {
                target.statusEffectList[tempIndex].ResetEffect(target);
                target.statusEffectList.RemoveAt(tempIndex);
            }
            else
            {
                tempIndex++;
            }
        }
    }
}
