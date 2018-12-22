using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_RefreshOverload : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_RefreshOverload(source, allChara);

    }
}
public class eAI_RefreshOverload : Action
{
    public eAI_RefreshOverload(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " removes all OVERLOAD from its team!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                int tempIndex = 0;
                while (tempIndex < chara.statusEffectList.Count)
                {
                    if (chara.statusEffectList[tempIndex].StatusID == 11)
                    {
                        chara.statusEffectList[tempIndex].ResetEffect(chara);
                        chara.statusEffectList.RemoveAt(tempIndex);
                    }
                    else
                    {
                        tempIndex++;
                    }
                }
            }
        }
    }
}
