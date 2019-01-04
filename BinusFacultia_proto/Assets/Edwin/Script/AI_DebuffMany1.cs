using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_DebuffMany1 : ActionInterface
{
    public int level;
    public int dur;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_DebuffMany1 act = new eAI_DebuffMany1(source, allChara);
        act.level = level;
        act.dur = dur;
        return act;
    }
}
public class eAI_DebuffMany1 : Action
{
    public int level;
    public int dur;
    private Chara target;
    public eAI_DebuffMany1(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " causes "+target.name+"'s stats to harshly decrease!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_AtkDOWN();
        StatusEffect se2 = new Ef_SpdDOWN();
        StatusEffect se3 = new Ef_DefDOWN();
        se.InitializeSE(level, dur);
        se2.InitializeSE(level, dur);
        se3.InitializeSE(level, dur);

        List<Chara> viableTarget = new List<Chara>();
        foreach(Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTarget.Add(chara);
            }
        }
        target = viableTarget[Random.Range(0, viableTarget.Count)];
        target.InflictStatus(se);
        target.InflictStatus(se2);
        target.InflictStatus(se3);
    }
}
