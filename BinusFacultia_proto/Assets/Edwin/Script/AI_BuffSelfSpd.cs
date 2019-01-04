using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffSelfSpd : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffSelfSpd act = new eAI_BuffSelfSpd(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffSelfSpd : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffSelfSpd(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " buffs its own Spd!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_SpdUP();
        se.InitializeSE(level, duration);
        source.InflictStatus(se);
    }
}
