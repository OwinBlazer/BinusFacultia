using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_OverloadTarget : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_OverloadTarget act = new eAI_OverloadTarget(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_OverloadTarget : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_OverloadTarget(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " targets "+ target.name +" with Overload!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_Poison();
        se.InitializeSE(level, duration);
        List<Chara> viableTargets = new List<Chara>();
        //random target, save to tauntTarget
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
        }
        target = viableTargets[Random.Range(0, viableTargets.Count)];
        target.InflictStatus(se);
        source.efTauntTarget = target;
    }
}
