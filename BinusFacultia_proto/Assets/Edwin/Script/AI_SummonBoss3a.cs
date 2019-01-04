using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_SummonBoss3a : ActionInterface
{
    public EnemyChara minion;
    public int number;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_SummonBoss3a action = new eAI_SummonBoss3a(source, allChara);
        action.minion = minion;
        action.number = number;
        return action;
    }
}
public class eAI_SummonBoss3a : Action
{
    public EnemyChara minion;
    public int number;
    public eAI_SummonBoss3a(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " summons "+number+" "+minion.chara.name+"!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        CombatEngine combatEngine = GameObject.FindObjectOfType<CombatEngine>();
        for (int i = 0; i < number; i++)
        {
            combatEngine.SpawnEnemy(minion);
        }
    }
}
