using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI_DebuffTauntHP : ActionInterface
{
    public int level;
    public int duration;
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        eAI_DebuffTauntHP act = new eAI_DebuffTauntHP(source, allChara);
        act.level = level;
        act.duration = duration;
        return act;
    }
}
public class eAI_DebuffTauntHP : Action
{
    public int level;
    public int duration;
    private Chara target;
    public eAI_DebuffTauntHP(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " devilishly inflicts Taunt on" + target.name + "!\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        StatusEffect se = new Ef_Taunt();
        int maxHP = 9999;
        se.InitializeSE(level, duration);
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                if (chara.HPcurr < maxHP)
                {
                    target = chara;
                    maxHP = chara.HPcurr;
                }
            }
        }
        if (maxHP < 9999)
        {
            target.InflictStatus(se);
        }
    }
}
