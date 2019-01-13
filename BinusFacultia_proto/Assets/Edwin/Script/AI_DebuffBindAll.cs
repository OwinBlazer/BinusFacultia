using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_DebuffBindAll : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_DebuffBindAll act = new eAI_DebuffBindAll(source, allChara);
        return act;
    }
}
public class eAI_DebuffBindAll : Action
{
    private Chara target;
    public eAI_DebuffBindAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " overwhelms the party with binding!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                StatusEffect se = new Ef_Stun();
                StatusEffect se2 = new Ef_SpdDOWN();
                se.InitializeSE(8, 3);
                se2.InitializeSE(5, 4);
                chara.InflictStatus(se);
                chara.InflictStatus(se2);
                chara.TakeDamage(CalculateDamage(source.atk,chara));
            }
        }

    }
}
