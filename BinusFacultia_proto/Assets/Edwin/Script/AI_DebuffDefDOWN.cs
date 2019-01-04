using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_DebuffDefDOWN : ActionInterface
{
    public int defDownLevel;
    public int defDownDur;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_DebuffDefDOWN act = new eAI_DebuffDefDOWN(source, allChara);
        act.defDownDur = defDownDur;
        act.defDownLevel = defDownLevel;
        return act;
    }
}
public class eAI_DebuffDefDOWN : Action
{
    public int defDownLevel;
    public int defDownDur;
    private Chara target;
    public eAI_DebuffDefDOWN(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " harshly reducted "+target.name+"'s defense!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        int minDef = -9999;
        StatusEffect se = new Ef_DefDOWN();
        se.InitializeSE(defDownLevel, defDownDur);
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                //target player with highest def
                if (minDef<chara.def)
                {
                    minDef = chara.def;
                    target = chara;
                }
            }
        }
        if (minDef > -9999)
        {
            target.InflictStatus(se);
        }

    }
}
