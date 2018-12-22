using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_HPTrade : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_HPTrade action = new eAI_HPTrade(source, allChara);
        return action;
    }
}
public class eAI_HPTrade : Action
{
    public eAI_HPTrade(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " grants used its own HP to grow!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        source.TakeDamage(1);
        source.HPmax += 3;
    }
}
