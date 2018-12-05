﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerChara : MonoBehaviour {
    public int[] trainTracker;
    public Chara chara;
    public List<SkillInterface> skillList;
}