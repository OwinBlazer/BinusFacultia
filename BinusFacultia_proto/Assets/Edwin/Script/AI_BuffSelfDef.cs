using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffSelfDef : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffSelfDef act = new eAI_BuffSelfDef(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffSelfDef : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffSelfDef(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " buffs its own Def!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_DefUP();
        se.InitializeSE(level, duration);
        source.InflictStatus(se);
    }
}
