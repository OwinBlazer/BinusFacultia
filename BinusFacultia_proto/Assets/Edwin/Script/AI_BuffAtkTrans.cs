using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_BuffAtkTrans : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_BuffAtkTrans action = new eAI_BuffAtkTrans(source, allChara);
        return action;
    }
}
public class eAI_BuffAtkTrans : Action
{
    public eAI_BuffAtkTrans(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " transfers its Atk to its team!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_AtkDOWN();
        se.InitializeSE(2,1);
        source.InflictStatus(se);
        se = new Ef_AtkUP();
        se.InitializeSE(5, 2);
        foreach(Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0 && chara!=source)
            {
                chara.InflictStatus(se);
            }
        }
    }
}
