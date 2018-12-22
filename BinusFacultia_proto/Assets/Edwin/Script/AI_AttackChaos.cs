using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AttackChaos : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_AttackChaos(source, allChara);

    }
}
public class eAI_AttackChaos : Action
{
    private int damage;
    private Chara targetSource;
    private Chara targetHit;
    public eAI_AttackChaos(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " makes " + targetSource.name + " punch " + targetHit.name+ " for "+damage + "\n";
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
        //makes source attack hit
        targetSource = viableTargets[Random.Range(0, viableTargets.Count)];
        targetHit = viableTargets[Random.Range(0, viableTargets.Count)];
        damage = CalculateDamage(targetSource.atk/2 , targetHit);
        targetHit.TakeDamage(damage);
    }
}
