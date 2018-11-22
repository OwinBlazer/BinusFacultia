using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillInterface : MonoBehaviour {
    public string skillName;
    public int mpCost;
    public int skillLevel;
    public abstract PlayerAction GetSkill(Chara source, List<Chara> allChara);
}
