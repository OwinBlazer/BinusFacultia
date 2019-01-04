using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffSelfAtk : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffSelfAtk act = new eAI_BuffSelfAtk(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffSelfAtk : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffSelfAtk(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " buffs its own Attack!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_AtkUP();
        se.InitializeSE(level, duration);
        source.InflictStatus(se);
    }
}
