using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffNextLevel : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffNextLevel act = new eAI_BuffNextLevel(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffNextLevel : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffNextLevel(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " intensifies with "+target.name+"!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        if (source.efTauntTarget != null)
        {
            if (source.efTauntTarget.HPcurr > 0)
            {
                target = source.efTauntTarget;
            }
            else
            {
                foreach (Chara chara in targetList)
                {
                    if (!chara.isEnemy && chara.HPcurr > 0)
                    {
                        viableTargets.Add(chara);
                    }
                }
                target = viableTargets[Random.Range(0, viableTargets.Count)];
            }
        }
        else
        {
            foreach (Chara chara in targetList)
            {
                if (!chara.isEnemy && chara.HPcurr > 0)
                {
                    viableTargets.Add(chara);
                }
            }
            target = viableTargets[Random.Range(0, viableTargets.Count)];
        }
        StatusEffect se = new Ef_AtkUP();
        se.InitializeSE(level, duration);
        target.InflictStatus(se);
        StatusEffect se2 = new Ef_SpdUP();
        se2.InitializeSE(level, duration);
        target.InflictStatus(se2);
        StatusEffect se3 = new Ef_DefUP();
        se3.InitializeSE(level, duration);
        target.InflictStatus(se3);
    }
}
