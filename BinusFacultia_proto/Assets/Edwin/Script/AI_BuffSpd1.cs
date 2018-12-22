using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffSpd1 : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffSpd1 act = new eAI_BuffSpd1(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffSpd1 : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffSpd1(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " boosts " + target.name + "'s speed!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
        }
        target = viableTargets[Random.Range(0, viableTargets.Count)];
        StatusEffect se = new Ef_SpdUP();
        se.InitializeSE(level, duration);
        target.InflictStatus(se);
    }
}
