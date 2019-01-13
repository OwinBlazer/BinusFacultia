using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffAtkSpdAll : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffAtkSpdAll act = new eAI_BuffAtkSpdAll(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffAtkSpdAll : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffAtkSpdAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " boosts all enemy's attack & speed!\n";
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
                StatusEffect se2 = new Ef_AtkUP();
                se2.InitializeSE(level, duration);
                chara.InflictStatus(se);
                chara.InflictStatus(se2);
            }
        }
    }
}
