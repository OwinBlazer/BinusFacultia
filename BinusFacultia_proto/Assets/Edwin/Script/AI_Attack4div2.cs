using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_Attack4div2 : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_Attack4div2(source, allChara);
    }
}
public class eAI_Attack4div2 : Action
{
    private int damage;
    private Chara target;
    public eAI_Attack4div2(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " attacks " + target.name + "repeatedly for a total of "+damage*4+"!\n";
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
        damage = CalculateDamage(source.atk/2, target);
        for (int i = 0; i < 4; i++)
        {
            target.TakeDamage(damage);
        }
    }
}
