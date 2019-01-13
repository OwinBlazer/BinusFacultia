using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class It03Revive : ItemInterface
{
    PlayerAction useItem;
    public override PlayerAction UseItem(Chara source)
    {
        useItem = new Item_Revive();
        useItem.source = source;
        useItem.needTarget = true;
        return useItem;
    }
}
public class Item_Revive : PlayerAction
{
    public override void executeAction()
    {
        target.HealDamage(target.HPmax/2);
    }

    public override List<Chara> GetValidTarget(List<Chara> allChara)
    {
        List<Chara> validTarget = new List<Chara>();
        foreach (Chara chara in allChara)
        {
            if (!chara.isEnemy && chara.HPcurr <= 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }

    public override void updateLog(Text targetBox)
    {
        targetBox.text += "The irresistable smell rescucitated " + target.name + "!\n";
    }
}
