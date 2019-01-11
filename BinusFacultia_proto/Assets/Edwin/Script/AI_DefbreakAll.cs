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
    public eAI_DefbreakAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " sprays your team with Def DOWN and amplified all status effects!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_DefDOWN();
        se.InitializeSE(defDownLevel, defDownDur);
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                //severity
                foreach (StatusEffect se2 in chara.statusEffectList)
                {
                    se2.ResetEffect(chara);
                    se2.level += sevMod;
                    se2.RunEffect(chara);
                }
                chara.InflictStatus(se);
            }
        }
        
    }
}
