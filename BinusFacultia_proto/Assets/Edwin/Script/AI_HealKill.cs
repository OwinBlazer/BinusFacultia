using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_HealKill : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_HealKill(source, allChara);

    }
}
public class eAI_HealKill : Action
{
    private int damage;
    private Chara target;
    private List<Chara> validTargets;
    public eAI_HealKill(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        targetBox.text += message;
    }
    public override void executeAction()
    {
        validTargets = new List<Chara>();
        int enemyNum = 0;
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                enemyNum++;
                if (chara != source && chara.GetFoeID() <= 4)
                {
                    validTargets.Add(chara);
                }
            }
        }
        if (validTargets.Count > 0)
        {
            //if there are actual targets
            target = validTargets[Random.Range(0, validTargets.Count)];
            message = source.name + " uses " + target.name + " to heal its team!\n";
            int healNum = target.HPcurr / (enemyNum - 1);
            target.TakeDamage(99999);
            foreach (Chara chara in targetList)
            {
                if (chara.isEnemy && chara.HPcurr > 0)
                {
                    chara.HealDamage(healNum);
                }
            }
        }
        else
        {
            //if there are no target
            message = source.name+"fumbled and failed to act!\n";
        }
        
    }
}
