using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffSpdAll : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffSpdAll act = new eAI_BuffSpdAll(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffSpdAll : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffSpdAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " boosts all enemy's speed!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        
        StatusEffect se = new Ef_SpdUP();
        se.InitializeSE(level, duration);
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                chara.InflictStatus(se);
            }
        }
    }
}
