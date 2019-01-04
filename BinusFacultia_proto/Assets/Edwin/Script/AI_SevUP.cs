using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_SevUP : ActionInterface
{
    public int level;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_SevUP act = new eAI_SevUP(source, allChara);
        act.level = level;
        return act;
    }
}
public class eAI_SevUP : Action
{
    public int level;
    private Chara target;
    public eAI_SevUP(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " increases " + target.name + "'s severity!\n";
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
        int indexFlag = 0;
        StatusEffect se;
        while (indexFlag < target.statusEffectList.Count)
        {
            se = target.statusEffectList[indexFlag];
            se.ResetEffect(target);
            se.level += level;
            se.RunEffect(target);
            indexFlag++;
        }
    }
}
