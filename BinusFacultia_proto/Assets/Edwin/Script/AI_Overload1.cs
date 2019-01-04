using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_Overload1 : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_Overload1 act = new eAI_Overload1(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_Overload1 : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_Overload1(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " inflicts " + target.name + " with Overload!\n";
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
    }
}
