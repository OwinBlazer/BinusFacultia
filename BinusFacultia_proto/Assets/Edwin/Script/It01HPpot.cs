using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class It01HPpot : ItemInterface
{
    PlayerAction useItem;
    public override PlayerAction UseItem(Chara source)
    {
        useItem = new Item_HPpot();
        useItem.source = source;
        useItem.needTarget = true;
        return useItem;
    }
}
public class Item_HPpot : PlayerAction
{
    public override void executeAction()
    {
        target.HealDamage(target.HPmax);
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
        targetBox.text += "The hearty meal recovered " + target.name + "'s HP to max!\n";
    }
}
