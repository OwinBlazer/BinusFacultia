using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_Attack4div2Target : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_Attack4div2Target(source, allChara);
    }
}
public class eAI_Attack4div2Target : Action
{
    private int damage;
    private Chara target;
    public eAI_Attack4div2Target(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " attacks " + target.name + "repeatedly for a total of " + damage * 4 + "!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        if (source.efTauntTarget != null)
        {
            if (source.efTauntTarget.HPcurr > 0)
            {
                target = source.efTauntTarget;
            }
            else
            {
                foreach (Chara chara in targetList)
                {
                    if (!chara.isEnemy && chara.HPcurr > 0)
                    {
                        viableTargets.Add(chara);
                    }
                }
                target = viableTargets[Random.Range(0, viableTargets.Count)];
            }
        }
        else
        {
            foreach (Chara chara in targetList)
            {
                if (!chara.isEnemy && chara.HPcurr > 0)
                {
                    viableTargets.Add(chara);
                }
            }
            target = viableTargets[Random.Range(0, viableTargets.Count)];
        }
        damage = CalculateDamage(source.atk / 2, target);
        for (int i = 0; i < 4; i++)
        {
            target.TakeDamage(damage);
        }
    }
}
