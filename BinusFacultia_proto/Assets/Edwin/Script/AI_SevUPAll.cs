using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_SevUPAll : ActionInterface
{
    public int level;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_SevUPAll act = new eAI_SevUPAll(source, allChara);
        act.level = level;
        return act;
    }
}
public class eAI_SevUPAll : Action
{
    public int level;
    private Chara target;
    public eAI_SevUPAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " increases the party's severity!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
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
    }
}
