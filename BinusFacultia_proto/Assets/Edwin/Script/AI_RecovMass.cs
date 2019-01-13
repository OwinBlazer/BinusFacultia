using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_RecovMass : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_RecovMass action = new eAI_RecovMass(source, allChara);
        action.level = level;
        action.duration = duration;
        return action ;

    }
}
public class eAI_RecovMass : Action
{
    public int level;
    public int duration;
    public eAI_RecovMass(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " grants Recovery to its team!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                StatusEffect se = new Ef_Regen();
                se.InitializeSE(level, duration);
                Debug.Log(se.isBuff);
                chara.InflictStatus(se);
            }
        }
    }
}
