using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_DebuffStunHP : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_DebuffStunHP act = new eAI_DebuffStunHP(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_DebuffStunHP : Action
{
    public int level;
    public int duration;
    public int damage;
    private Chara target;
    public eAI_DebuffStunHP(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " inflicts harsh Stun on" + target.name +"with damage of"+ damage+"!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_Stun();
        int minHP = -9999;
        se.InitializeSE(level, duration);
        target.InflictStatus(se);
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                if (chara.HPcurr > minHP)
                {
                    target = chara;
                    minHP = chara.HPcurr;
                }
            }
        }
        if (minHP > -9999)
        {
            target.InflictStatus(se);
            damage = CalculateDamage(source.atk, target);
            target.TakeDamage(damage);
        }
    }
}
