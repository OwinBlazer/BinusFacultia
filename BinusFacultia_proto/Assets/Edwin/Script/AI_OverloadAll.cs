using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_OverloadAll : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_OverloadAll act = new eAI_OverloadAll(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_OverloadAll : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_OverloadAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " inflicts party with Overload!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_Poison();
        se.InitializeSE(level, duration);
        foreach(Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                chara.InflictStatus(se);
            }
        }
    }
}
