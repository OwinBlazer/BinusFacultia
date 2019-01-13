using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_SevUPSelf : ActionInterface
{
    public int level;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_SevUPSelf act = new eAI_SevUPSelf(source, allChara);
        act.level = level;
        return act;
    }
}
public class eAI_SevUPSelf : Action
{
    public int level;
    private Chara target;
    public eAI_SevUPSelf(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " increases its own severity!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        GameObject.FindObjectOfType<CombatEngine>().PlayBuffFX(source);
        int indexFlag = 0;
        StatusEffect se;
        while (indexFlag < source.statusEffectList.Count)
        {
            se = source.statusEffectList[indexFlag];
            se.ResetEffect(source);
            se.level += level;
            se.RunEffect(source);
            indexFlag++;
        }
    }
}
