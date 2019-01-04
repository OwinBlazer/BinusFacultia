using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AttackBomb : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_AttackBomb(source, allChara);
    }
}
public class eAI_AttackBomb : Action
{
    private int damage;
    public eAI_AttackBomb(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " tries to take the party out with it!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                damage = CalculateDamage(source.atk, chara);
                chara.TakeDamage(damage);
            }
        }
        source.TakeDamage(9999);
    }
}
