using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class It04Refresh : ItemInterface
{
    PlayerAction useItem;
    public override PlayerAction UseItem(Chara source)
    {
        useItem = new Item_Refresh();
        useItem.source = source;
        useItem.needTarget = true;
        return useItem;
    }
}
public class Item_Refresh : PlayerAction
{
    public override void executeAction()
    {
        //remove all negative status from target's selist
        int tempIndex = 0;
        while (tempIndex < target.statusEffectList.Count)
        {
            if (target.statusEffectList[tempIndex].isBuff)
            {
                target.statusEffectList[tempIndex].ResetEffect(target);
                target.statusEffectList.RemoveAt(tempIndex);
            }
            else
            {
                tempIndex++;
            }
        }
    }

    public override List<Chara> GetValidTarget(List<Chara> allChara)
    {
        List<Chara> validTarget = new List<Chara>();
        foreach (Chara chara in allChara)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }

    public override void updateLog(Text targetBox)
    {
        targetBox.text += "The popular pasta removed " + target.name + "'s Negative Effects!\n";
    }
}
