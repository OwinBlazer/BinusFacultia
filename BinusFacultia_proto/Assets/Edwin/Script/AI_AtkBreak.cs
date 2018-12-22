using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AtkBreak : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_AtkBreak act = new eAI_AtkBreak(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_AtkBreak : Action
{
    public int level;
    public int duration;
    private int damage;
    public eAI_AtkBreak(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " damages your team with Atk DOWN!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_AtkDOWN();
        se.InitializeSE(level, duration);
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                chara.TakeDamage(CalculateDamage(source.atk / 2, chara));
                chara.InflictStatus(se);
            }
        }
    }
}
