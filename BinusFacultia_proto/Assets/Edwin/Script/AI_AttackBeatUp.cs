using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AttackBeatUp : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_AttackBeatUp(source, allChara);

    }
}
public class eAI_AttackBeatUp : Action
{
    private int damage;
    private Chara target;
    public eAI_AttackBeatUp(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " rushes together at " + target.name + " for " + damage + "\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        int skillMod = 0;
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
            if(chara.isEnemy && chara.HPcurr > 0) {
                //counts the number of enemy alive
                skillMod++;
            }
        }
        target = viableTargets[Random.Range(0, viableTargets.Count)];
        damage = CalculateDamage(source.atk*skillMod/2, target);
        target.TakeDamage(damage);
    }
}
