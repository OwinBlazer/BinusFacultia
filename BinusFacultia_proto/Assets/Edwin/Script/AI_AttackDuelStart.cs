using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AttackDuelStart : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_AttackDuelStart(source, allChara);
    }
}
public class eAI_AttackDuelStart : Action
{
    private int damage;
    private Chara target;
    public eAI_AttackDuelStart(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " attacks for "+damage+" and starts a duel with "+target.name+"!\n";
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
        foreach(StatusEffect se in target.statusEffectList)
        {
            se.ResetEffect(target);
        }
        target.statusEffectList.Clear();
        StatusEffect se2 = new Ef_Taunt();
        se2.InitializeSE(8, 3);
        target.InflictStatus(se2);
        damage = CalculateDamage(source.atk, target);
        target.TakeDamage(damage);
        source.efTauntTarget = target;
    }
}
