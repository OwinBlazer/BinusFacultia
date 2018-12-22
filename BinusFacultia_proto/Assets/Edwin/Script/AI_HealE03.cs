using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_HealE03 : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        Action action = new eAI_HealE03(source, allChara);
        return action;
    }
}
public class eAI_HealE03 : Action
{
    public eAI_HealE03(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " heals all Lupa!!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach(Chara chara in targetList)
        {
            if (chara.isEnemy&&chara.HPcurr>0)
            {
                if (chara.GetFoeID()==3)
                {
                    chara.HealDamage(3);
                }
            }
        }
    }
}
