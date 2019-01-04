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
    private float tauntSum = 0;
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
        //check taunt
        tauntSum = 0;

        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
                tauntSum += chara.efTauntRate;
            }
        }
        if (tauntSum > 0)
        {
            //taunted attack
            float rng = Random.Range(0, tauntSum);
            int index = 0;
            while (rng>viableTargets[index].efTauntRate)
            {
                rng -= viableTargets[index].efTauntRate;
                index++;
            }
            target = viableTargets[index];
        }
        else
        {
            target = viableTargets[Random.Range(0, viableTargets.Count)];
        }
        damage = CalculateDamage(source.atk, target);
        target.TakeDamage(damage);
    }
}
