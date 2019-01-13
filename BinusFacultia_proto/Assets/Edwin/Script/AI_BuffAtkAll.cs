using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffAtkAll : ActionInterface
{
    public int level, duration;

    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffAtkAll action = new eAI_BuffAtkAll(source, allChara);
        action.level = level;
        action.duration = duration;
        return action;
    }
}
public class eAI_BuffAtkAll : Action
{
    public int level, duration;
    public eAI_BuffAtkAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " increases all enemies' attack!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se;
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                se = new Ef_AtkUP();
                se.InitializeSE(level, duration);
                chara.InflictStatus(se);
            }
        }
    }
}
