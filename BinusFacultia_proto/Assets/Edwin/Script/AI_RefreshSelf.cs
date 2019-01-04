using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_RefreshSelf : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_RefreshSelf(source, allChara);

    }
}
public class eAI_RefreshSelf : Action
{
    private int damage;
    private Chara target;
    public eAI_RefreshSelf(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " removes all status effect from itself!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        foreach (StatusEffect se in source.statusEffectList)
        {
            se.ResetEffect(source);
        }
        source.statusEffectList.Clear();
    }
}
