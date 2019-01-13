using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_DebuffSpdAll : ActionInterface
{
    public int level;
    public int dur;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_DebuffSpdAll act = new eAI_DebuffSpdAll(source, allChara);
        act.dur = dur;
        act.level = level;
        return act;
    }
}
public class eAI_DebuffSpdAll : Action
{
    public int level;
    public int dur;
    private Chara target;
    public eAI_DebuffSpdAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " inflicted Spd DOWN on all party members!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                StatusEffect se = new Ef_SpdDOWN();
                se.InitializeSE(level, dur);
                chara.InflictStatus(se);
            }
        }

    }
}
