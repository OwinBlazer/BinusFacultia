using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_AttackBloodLust : ActionInterface
{
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new eAI_AttackBloodLust(source, allChara);
    }
}
public class eAI_AttackBloodLust : Action
{
    private int damage;
    private Chara target;
    public eAI_AttackBloodLust(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " lets loose!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        int PlayerNum=0;
        foreach(Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                PlayerNum++;
            }
        }
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                for(int i = 0; i < PlayerNum;i++)
                {
                    chara.TakeDamage(CalculateDamage((source.atk - 2) / PlayerNum, chara));
                }
            }
        }
    }
}
