using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerChara : MonoBehaviour {
    public Chara chara;
    public List<SkillInterface> skillList;
    public int trainCount;
    public int spCount;
}
