using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e8_CustomAI : MonoBehaviour {
    EnemyChara enemy;
    // Update is called once per frame
    private void Start()
    {
        enemy = GetComponent<EnemyChara>();
    }
    void Update () {
        if (enemy.chara.HPcurr < 20)
        {
            enemy.chara.sequence[2].value = 3;
        }
	}
}
