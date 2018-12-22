using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_HealSelf : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_HealSelf action = new eAI_HealSelf(source, allChara);
        return action;
    }
}
public class eAI_HealSelf : Action
{
    public eAI_HealSelf(Chara source, List<Chara> targetList)
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
        source.HealDamage(source.HPcurr * 2);
    }
}
