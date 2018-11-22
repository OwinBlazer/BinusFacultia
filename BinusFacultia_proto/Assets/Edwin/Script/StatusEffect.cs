using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect {
    int level;
    int duration;
    public abstract void RunEffect(Chara target);
}

public class Ef_AtkUP:StatusEffect {
    public override void RunEffect(Chara target)
    {
        target.atk = target.baseAtk * 2;
    }
}

