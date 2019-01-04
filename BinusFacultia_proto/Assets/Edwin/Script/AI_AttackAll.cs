using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AttackAll : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_AttackAll(source, allChara);
    }
}
public class eAI_AttackAll : Action
{
    private int damage;
    public eAI_AttackAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " Attacks the entire party at once!\n";
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
    }
}
