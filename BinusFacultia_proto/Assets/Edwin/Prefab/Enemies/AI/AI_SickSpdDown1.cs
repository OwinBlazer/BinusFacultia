using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_SickSpdDown1 : ActionInterface
{
    public int sickLevel;
    public int sickDur;
    public int spdDownLevel;
    public int spdDownDur;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_SickSpdDown1 act = new eAI_SickSpdDown1(source, allChara);
        act.sickLevel = sickLevel;
        act.sickDur = sickDur;
        act.spdDownDur = spdDownDur;
        act.spdDownLevel = spdDownLevel;
        return act;
    }
}
public class eAI_SickSpdDown1 : Action
{
    public int sickLevel;
    public int sickDur;
    public int spdDownLevel;
    public int spdDownDur;
    private Chara target;
    public eAI_SickSpdDown1(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " inflicts " + target.name + " with sick and Spd DOWN!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
        }
        target = viableTargets[Random.Range(0, viableTargets.Count)];
        StatusEffect se = new Ef_SpdDOWN();
        se.InitializeSE(spdDownLevel, spdDownDur);
        target.InflictStatus(se);
        se = new Ef_HPDOWN();
        se.InitializeSE(sickLevel, sickDur);
        target.InflictStatus(se);
    }
}
