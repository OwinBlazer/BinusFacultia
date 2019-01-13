using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffSpdDefAll : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffSpdDefAll act = new eAI_BuffSpdDefAll(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffSpdDefAll : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffSpdDefAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " boosts all enemy's defense & speed!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {

        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                StatusEffect se = new Ef_SpdUP();
                se.InitializeSE(level, duration);
                StatusEffect se2 = new Ef_DefUP();
                se2.InitializeSE(level, duration);
                chara.InflictStatus(se);
                chara.InflictStatus(se2);
            }
        }
    }
}
