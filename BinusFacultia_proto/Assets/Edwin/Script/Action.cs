using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]//<===========how to put action into inspector?????@
public abstract class Action
{
    public string message;
    public Chara source;
    public List<Chara> targetList = new List<Chara>();
    public abstract void executeAction();
    public abstract void updateLog(Text targetBox);
    public int CalculateDamage(int atk, Chara target)
    {

        if (target.efShield > 0)
        {
            return 0;
        }
        else
        {

            int dmg = atk - target.def;

            if (target.isDefending)
            {
                dmg /= 2;
            }
            if (dmg <= 0)
            {
                dmg = 1;
            }
            return dmg;
        }
    }
}
public abstract class PlayerAction : Action
{
    public bool needTarget = true;
    public abstract List<Chara> GetValidTarget(List<Chara> allChara);
    public Chara target;
    public void setTarget(Chara target)
    {
        this.target = target;
    }
}
public class paAttack : PlayerAction
{
    private int damage;
    public override void executeAction()
    {
        damage = CalculateDamage(source.atk, target);
        target.TakeDamage(damage);
    }

    public override void updateLog(Text targetBox)
    {
        message = source.name + " attacks " + target.name + " for " + damage + " damage!\n";
        targetBox.text += message;
    }

    public override List<Chara> GetValidTarget(List<Chara> allChara)
    {
        List<Chara> validTarget = new List<Chara>();
        foreach (Chara chara in allChara)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                validTarget.Add(chara);
            }
        }
        return validTarget;
    }
}
public abstract class paSkill : PlayerAction
{
    public int level;
    public int mpCost;
}

public class AttackFoe : Action
{
    private Chara target;
    private int damage;
    public AttackFoe(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " attacks " + target.name + " for " + damage+"\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        foreach (Chara chara in targetList)
        {
            if (chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
        }
        int rng = Random.Range(0, viableTargets.Count);
        damage = CalculateDamage(source.atk, viableTargets[rng]);
        viableTargets[rng].TakeDamage(damage);
        target = viableTargets[rng];
    }
}
public class AttackPlayer : Action
{
    private int damage;
    private Chara target;
    public AttackPlayer(Chara source, List<Chara> targetList)
    {
        this.source = source;
        this.targetList = targetList;
    }
    public override void updateLog(Text targetBox)
    {
        message = source.name + " attacks " + target.name + " for " + damage + "\n";
        targetBox.text += message;
    }
    public override void executeAction()
    {
        List<Chara> viableTargets = new List<Chara>();
        foreach (Chara chara in targetList)
        {
            if (!chara.isEnemy && chara.HPcurr > 0)
            {
                viableTargets.Add(chara);
            }
        }
        int rng = Random.Range(0, viableTargets.Count);
        damage = CalculateDamage(source.atk, viableTargets[rng]);
        viableTargets[rng].TakeDamage(damage);
        target = viableTargets[rng];
    }
}
