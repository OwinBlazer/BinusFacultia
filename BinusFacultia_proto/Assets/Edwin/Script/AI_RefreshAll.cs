using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_RefreshAll : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_RefreshAll(source, allChara);

    }
}
public class eAI_RefreshAll : Action
{
    private int damage;
    private Chara target;
    public eAI_RefreshAll(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " removes all status effect from its team!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                foreach(StatusEffect se in chara.statusEffectList)
                {
                    se.ResetEffect(chara);
                }
                chara.statusEffectList.Clear();
                GameObject.FindObjectOfType<CombatEngine>().PlayHealFX(0, chara, 1);
            }
        }
    }
}
