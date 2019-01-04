using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffHPTransShield : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffHPTransShield act = new eAI_BuffHPTransShield(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_BuffHPTransShield : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_BuffHPTransShield(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " uses its lifeforce as Shield!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        source.TakeDamage(2);
        if (source.HPcurr > 0)
        {
            StatusEffect se = new Ef_Shield();
            se.InitializeSE(level, duration);
            target.InflictStatus(se);
        }
    }
}
