using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AttackRefreshSelf : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_AttackRefreshSelf(source, allChara);
    }
}
public class eAI_AttackRefreshSelf : Action
{
    private int damage;
    private Chara target;
    public eAI_AttackRefreshSelf(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " attacks " + target.name + " for " + damage + ", clearing its own debuffs!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        int tempIndex = 0;
        while (tempIndex < source.statusEffectList.Count)
        {
            if (source.statusEffectList[tempIndex].StatusID > 4 && source.statusEffectList[tempIndex].StatusID != 12 && source.statusEffectList[tempIndex].StatusID != 13 && source.statusEffectList[tempIndex].StatusID != 14)
            {
                source.statusEffectList[tempIndex].ResetEffect(source);
                source.statusEffectList.RemoveAt(tempIndex);
            }
            else
            {
                tempIndex++;
            }
        }
        List<Chara> viableTargets = new List<Chara>();
        foreach(Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
        }
        target = viableTargets[Random.Range(0, viableTargets.Count)];
        damage = CalculateDamage(source.atk, target);
        target.TakeDamage(damage);

        
    }
}
