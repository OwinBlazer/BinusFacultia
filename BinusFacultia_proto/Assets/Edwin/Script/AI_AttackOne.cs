using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackOne : ActionInterface {
    public override Action GetAction(Chara source, List<Chara> allChara)
    {
        return new AttackPlayer(source, allChara);
    }
}
