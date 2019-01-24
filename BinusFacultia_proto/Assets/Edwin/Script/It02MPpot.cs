using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class It02MPpot : ItemInterface
{
    Item_MPpot useItem;
    public override PlayerAction UseItem(Chara source)
    {
        useItem = new Item_MPpot();
        useItem.source = source;
        useItem.needTarget = true;
        useItem.itemHandler = this;
        return useItem;
    }
}
public class Item_MPpot : PlayerAction
{
    string message;
    public ItemInterface itemHandler;
    public override void executeAction()
    {
        if (itemHandler.GetQty() > 0)
        {
            target.HealMP(target.HPmax);
            itemHandler.changeQtyBy(-1);
            PlayerPrefs.SetInt("item02", itemHandler.GetQty());
            message = "The tasty meal recovered " + target.name + "'s MP to max!\n";
        }
        else
        {
            message = "Party ran out of " + itemHandler.GetName();
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
        targetBox.text += message;
    }
}
