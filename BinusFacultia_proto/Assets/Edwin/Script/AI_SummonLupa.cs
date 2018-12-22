using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_SummonLupa : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_SummonLupa action = new eAI_SummonLupa(source, allChara);
        return action;
    }
}
public class eAI_SummonLupa : Action
{

    public eAI_SummonLupa(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " summons a Lupa!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        CombatEngine combatEngine = GameObject.FindObjectOfType<CombatEngine>();
        combatEngine.SpawnEnemy(3);
    }
}
