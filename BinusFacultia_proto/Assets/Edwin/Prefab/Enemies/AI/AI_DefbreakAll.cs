using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_DefbreakAll : ActionInterface
{
    public int defDownLevel;
    public int defDownDur;
    public int sevMod;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_DefbreakAll act = new eAI_DefbreakAll(source, allChara);
        act.defDownDur = defDownDur;
        act.defDownLevel = defDownLevel;
        act.sevMod = sevMod;
        return act;
    }
}
public class eAI_DefbreakAll : Action
{
    public int defDownLevel;
    public int defDownDur;
    public int sevMod;
    private Chara target;
    public eAI_DefbreakAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " inflicts " + target.name + " with Def DOWN and amplified all status effects!\n";
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
        StatusEffect se = new Ef_DefDOWN();
        se.InitializeSE(defDownLevel, defDownDur);
        target.InflictStatus(se);
        //severity
        foreach (StatusEffect se2 in target.statusEffectList)
        {
            se2.ResetEffect(target);
            se2.level += sevMod;
            se2.RunEffect(target);
        }
    }
}
