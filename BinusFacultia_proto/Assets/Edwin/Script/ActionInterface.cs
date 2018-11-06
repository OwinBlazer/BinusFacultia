using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ActionSequence
{
    public int value;
    public ActionInterface[] actions;
}
public abstract class ActionInterface : MonoBehaviour {
    public abstract Action GetAction(Chara source, List<Chara> allChara);
}
