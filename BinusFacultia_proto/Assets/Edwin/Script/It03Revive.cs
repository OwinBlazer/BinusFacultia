using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class It03Revive : ItemInterface
{
    Item_Revive useItem;
    public override PlayerAction UseItem(Chara source)
    {
        useItem = new Item_Revive();
        useItem.source = source;
        useItem.needTarget = true;
        useItem.itemHandler = this;
        return useItem;
    }
}
public class Item_Revive : PlayerAction
{
    string message;
    public ItemInterface itemHandler;
    public override void executeAction()
    {
        if (itemHandler.GetQty() > 0)
        {
            target.HealDamage(target.HPmax / 2);
            itemHandler.changeQtyBy(-1);
            PlayerPrefs.SetInt("item03", itemHandler.GetQty());
            message = "The irresistable smell rescucitated " + target.name + "!\n";
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
            if (!chara.isEnemy && chara.HPcurr <= 0)
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
