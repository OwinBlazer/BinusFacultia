using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AttackOne : ActionInterface {
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new AttackPlayer(source, allChara);
    }
}
public class AttackPlayer : Action
{
    private int damage;
    private Chara target;
    public AttackPlayer(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " attacks " + target.name + " for " + damage + "\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
        }
        target = viableTargets[Random.Range(0, viableTargets.Count)];
        damage = CalculateDamage(source.atk, target);
        target.TakeDamage(damage);
    }
}
