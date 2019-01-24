using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class It01HPpot : ItemInterface
{
    Item_HPpot useItem;
    public override PlayerAction UseItem(Chara source)
    {
        useItem = new Item_HPpot();
        useItem.source = source;
        useItem.needTarget = true;
        useItem.itemHandler = this;
        return useItem;
    }
}
public class Item_HPpot : PlayerAction
{
    string message;
    public ItemInterface itemHandler;
    public override void executeAction()
    {
        if (itemHandler.GetQty() > 0)
        {
            target.HealDamage(target.HPmax);
            itemHandler.changeQtyBy(-1);
            PlayerPrefs.SetInt("item01", itemHandler.GetQty());
            message = "The hearty meal recovered " + target.name + "'s HP to max!\n";
        }
        else
        {
            message = "Party ran out of "+itemHandler.GetName();
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
