using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect {
    public int level;
    public int duration;
    public int StatusID;
    /*status effectID list:
    1 = Ef_AtkUP Up
    11 = Poison(Overload)
    12 = Shield
    13 = Extend
    */
    public abstract void InitializeSE(int level, int duration);
    public abstract void RunEffect(Chara target);
    public abstract void ReduceDur(Chara target);
    public abstract void ResetEffect(Chara target);
    public void ChangeDurBy(int x)
    {
        duration += x;
    }
    public int GetDur()
    {
        return duration;
    }

}

public class Ef_AtkUP:StatusEffect {
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 1;
        }
    }
    public override void RunEffect(Chara target)
    {
        target.atk = target.baseAtk + (int)Mathf.Floor(Mathf.Pow(level, 1.5f)); ;
    }
    public override void ReduceDur(Chara target)
    {
        ChangeDurBy(-1);
        if (GetDur() <= 0)
        {
            ResetEffect(target);
        }
    }
    public override void ResetEffect(Chara target)
    {
        target.atk = target.baseAtk;
    }
}

public class Ef_Poison : StatusEffect
{
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 11;
        }
    }
    public override void RunEffect(Chara target)
    {
        target.efPoison = (int)Mathf.Floor(Mathf.Pow(level, 1.5f));
    }
    public override void ReduceDur(Chara target)
    {
        ChangeDurBy(-1);
        if (GetDur() <= 0)
        {
            ResetEffect(target);
        }
    }
    public override void ResetEffect(Chara target)
    {
        target.efPoison = 0;
    }
}

public class Ef_Shield : StatusEffect
{
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 12;
        }
    }
    public override void RunEffect(Chara target)
    {
        target.efShield = level;
    }
    public override void ReduceDur(Chara target)
    {
        ChangeDurBy(-1);
        if (GetDur() <= 0)
        {
            ResetEffect(target);
        }
    }
    public override void ResetEffect(Chara target)
    {
        target.efShield = 0;
    }
}

public class Ef_Extend : StatusEffect
{
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 13;
        }
    }
    public override void RunEffect(Chara target)
    {
        target.efExtend = level;
    }
    public override void ReduceDur(Chara target)
    {
        ChangeDurBy(-1);
        if (GetDur() <= 0)
        {
            ResetEffect(target);
        }
    }
    public override void ResetEffect(Chara target)
    {
        target.efExtend = 0;
    }
}