using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_HealAdapt : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_HealAdapt(source, allChara);
    }
}
public class eAI_HealAdapt : Action
{
    private Chara target;
    private int healNum;
    private List<Chara> validTargets;
    public eAI_HealAdapt(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        targetBox.text += message;
    }
    public override void executeAction()
    {
        healNum = 0;
        foreach(Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                healNum++;
            }
        }
        healNum *= 4;
        source.HealDamage(healNum);
        message += source.name+" heals itself for "+healNum;
    }
}
