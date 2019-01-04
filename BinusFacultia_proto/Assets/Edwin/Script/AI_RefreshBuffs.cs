using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_RefreshBuffs : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_RefreshBuffs(source, allChara);

    }
}
public class eAI_RefreshBuffs : Action
{
    public eAI_RefreshBuffs(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " removes all BUFFS from "+targetBox.name+"!\n";
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
                    if (chara.statusEffectList[tempIndex].StatusID == 12|| chara.statusEffectList[tempIndex].StatusID == 14|| chara.statusEffectList[tempIndex].StatusID < 4)
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
