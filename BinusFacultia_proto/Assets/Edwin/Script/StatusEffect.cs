using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect {
    public int level;
    public int duration;
    public int StatusID;
    public bool isBuff;
    /*status effectID list:
    1 = AtkUP
    2 = SpdUP
    3 = DefUP
    4 = AtkDOWN
    5 = SpdDOWN
    6 = DefDOWN
    7 = HPDOwn
    11 = Poison(Overload)
    12 = Shield
    13 = Extend
    14 = Regen
    15 = Stun
    16 = Taunt
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
    private int effect;
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 1;
            isBuff = true;
        }
    }
    public override void RunEffect(Chara target)
    {
        effect = (int)Mathf.Floor(Mathf.Pow(level + 1, 1.5f));
        target.atk +=  effect;
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
        target.atk -=effect;
    }
}
public class Ef_SpdUP : StatusEffect
{
    private int effect;
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 2;
            isBuff = true;
        }
    }
    public override void RunEffect(Chara target)
    {
        effect = (int)Mathf.Floor(Mathf.Pow(level + 1, 1.5f));
        target.spd +=  effect;
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
        target.spd -= effect;
    }
}
public class Ef_DefUP : StatusEffect
{
    private int effect;
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 3;
            isBuff = true;
        }
    }
    public override void RunEffect(Chara target)
    {
        effect = (int)Mathf.Floor(Mathf.Pow(level + 1, 1.5f));
        target.def += effect ;
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
        target.def -=effect;
    }
}
public class Ef_AtkDOWN : StatusEffect
{
    private int effect;
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 4;
            isBuff = false;
        }
    }
    public override void RunEffect(Chara target)
    {
        effect = (int)Mathf.Floor(Mathf.Pow(level + 1, 1.5f));
        target.atk -= effect ;
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
        target.atk += effect;
    }
}
public class Ef_SpdDOWN : StatusEffect
{
    private int effect;
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 5;
            isBuff = false;
        }
    }
    public override void RunEffect(Chara target)
    {
        effect = (int)Mathf.Floor(Mathf.Pow(level + 1, 1.5f));
        target.spd -= effect ;
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
        target.spd += effect;
    }
}
public class Ef_DefDOWN : StatusEffect
{
    private int effect;
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 6;
            isBuff = false;
        }
    }
    public override void RunEffect(Chara target)
    {
        effect = (int)Mathf.Floor(Mathf.Pow(level + 1, 1.5f));
        target.def -= effect ;
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
        target.def += effect;
    }
}
public class Ef_HPDOWN : StatusEffect
{
    private int effect;
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 7;
            isBuff = false;
        }
    }
    public override void RunEffect(Chara target)
    {
        effect = (int)Mathf.Floor(Mathf.Pow(level + 1, 1.5f));
        target.HPmax -= effect;
        if (target.HPmax < target.HPcurr) { target.TakeDamage(target.HPcurr - target.HPmax); }
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
        target.HPmax += effect;
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
            isBuff = false;
        }
    }
    public override void RunEffect(Chara target)
    {
        target.efPoison = (int)Mathf.Floor(Mathf.Pow(level+1, 1.5f));
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
            isBuff = true;
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
            isBuff = true;
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

public class Ef_Regen : StatusEffect
{
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 14;
            isBuff = true;
        }
    }
    public override void RunEffect(Chara target)
    {
        target.efRecov = (int)Mathf.Floor(Mathf.Pow(level + 1, 1.5f));
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
        target.efRecov = 0;
    }
}

public class Ef_Stun : StatusEffect
{
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 15;
            isBuff = false;
        }
    }
    public override void RunEffect(Chara target)
    {
        target.efStunRate = level/(level+2);
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
        target.efStunRate = 0;
    }
}

public class Ef_Taunt : StatusEffect
{
    public override void InitializeSE(int level, int duration)
    {

        {
            this.level = level;
            this.duration = duration;
            this.StatusID = 16;
            isBuff = false;
        }
    }
    public override void RunEffect(Chara target)
    {
        target.efTauntRate = level / (level + 2);
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
        target.efTauntRate = 0;
    }
}