using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_DebuffDefAll : ActionInterface
{
    public int level;
    public int dur;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_DebuffDefAll act = new eAI_DebuffDefAll(source, allChara);
        act.dur = dur;
        act.level = level;
        return act;
    }
}
public class eAI_DebuffDefAll : Action
{
    public int level;
    public int dur;
    private Chara target;
    public eAI_DebuffDefAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " inflicted Def DOWN on all party members!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                StatusEffect se = new Ef_DefDOWN();
                se.InitializeSE(level, dur);
                chara.InflictStatus(se);
            }
        }

    }
}
