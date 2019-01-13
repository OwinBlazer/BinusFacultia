using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffDefAll : ActionInterface
{
    public int level, duration;

    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffDefAll action = new eAI_BuffDefAll(source, allChara);
        action.level = level;
        action.duration = duration;
        return action;
    }
}
public class eAI_BuffDefAll : Action
{
    public int level, duration;
    public eAI_BuffDefAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " increases all enemies' defense!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                StatusEffect se = new Ef_DefUP();
                se.InitializeSE(level, duration);
                chara.InflictStatus(se);
            }
        }
    }
}
