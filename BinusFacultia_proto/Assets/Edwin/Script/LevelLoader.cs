using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    public CombatEngine combatEngine;
    public int wave;
    public int semester;
    [SerializeField] Semester[] bossList;
    [SerializeField]public Semester[] semesterList;
	// Use this for initialization

    
    public void initSession()
    {
        string sessionData = PlayerPrefs.GetString("sessionDetails", "");
        string[] sessionDetail = sessionData.Split('#');
        if (sessionDetail.Length < 3)
        {
            //no data found
            wave = 0;
            semester = 1;
            combatEngine.combatInProgress = true;
            PlayerPrefs.SetString("sessionDetails","0#1#1");
        }
        else
        {
            //data found
            //set wave and semester accordingly
            wave = int.Parse(sessionDetail[0]);
            semester = int.Parse(sessionDetail[1]);
            //set combatInProgress accordingly
            if (int.Parse(sessionDetail[2]) == 0)
            {
                combatEngine.combatInProgress = false;
            }
            else
            {
                combatEngine.combatInProgress = true;
            }
        }
        //load enemies as stored
        loadEnemies();
    }
    public void SaveSession()
    {
        //    wave = 0;
        //    semester = 1;
        //    combatEngine.combatInProgress = true;
        string saveData = "";
        saveData +=wave+"#";
        saveData += semester + "#";
        if (combatEngine.combatInProgress)
        {
            saveData += "1";
        }
        else
        {
            saveData += "0";
        }
        PlayerPrefs.SetString("sessionDetails", saveData);
    }
public int GetWave()
    {
        return wave;
    }
    public int GetSem()
    {
        return semester;
    }
    public int NextWaveSem()
    {
        wave++;

        if (wave > 4)
        {
            wave = 0;
            semester++;
            //@@Between Semester Scene
            //
            Debug.Log("Next Semester Loads!");
        }
        else
        {
            if (wave <= 3)
            {
                loadEnemies();
            }
            else if (wave == 4)
            {
                loadBoss();
            }
        }
        
        return wave;
    }
    public void loadBoss()
    {
        foreach(EnemyChara echara in bossList[semester - 1].spawnGroup[0].eCharaList)
        {
            combatEngine.SpawnEnemy(echara);
        }
    }
    public void loadEnemies()
    {
        //Debug.Log(combatEngine.combatInProgress);
        if (combatEngine.combatInProgress)
        {
            //load already there units
            string enemyData = PlayerPrefs.GetString("enemyDetails", "");
            string[] enemyUnit = enemyData.Split('\n');
            if (enemyUnit.Length <= 5)
            {
                //error handling: data not found
                combatEngine.combatInProgress = false;
                loadEnemies();
            }
            else
            {
                //data found, load 5 enemies
                for(int i = 0; i < 5; i++)
                {
                    string[] enemyDetail = enemyUnit[i].Split('#');
                    //Debug.Log("Loading data "+i+" found qty "+enemyDetail.Length);
                    //string debugger = "";
                    //foreach(string s in enemyDetail)
                    //{
                    //    debugger += s;
                    //    debugger += " ";
                    //}
                    //Debug.Log("Debugger: "+debugger);
                    //loads id, stats, moves based on id
                    EnemyChara eChara = combatEngine.SpawnEnemy(int.Parse(enemyDetail[1]));
                    //loads name
                    eChara.chara.name = enemyDetail[0];

                    //loads currhp
                    eChara.chara.HPcurr = int.Parse(enemyDetail[2]);

                    //loads battle stats
                    eChara.chara.efExtend = int.Parse(enemyDetail[3]);
                    eChara.chara.efPoison = int.Parse(enemyDetail[4]);
                    eChara.chara.efRecov = int.Parse(enemyDetail[5]);
                    eChara.chara.efShield = int.Parse(enemyDetail[6]);
                    eChara.chara.efStunRate = int.Parse(enemyDetail[7]);
                    eChara.chara.efTauntRate = int.Parse(enemyDetail[8]);
                    string targetName = enemyDetail[9];
                    if (targetName != "none")
                    {
                        foreach (Chara chara in combatEngine.GetAllChara())
                        {
                            if (chara.name == targetName)
                            {
                                eChara.chara.efTauntTarget = chara;
                            }
                        }
                    }

                    //loads status effect
                    if (enemyDetail[10].Length > 0)
                    {
                        string[] seData = enemyDetail[10].Split('&');
                        Debug.Log(seData.Length);
                        foreach (string seSet in seData)
                        {
                            string[] seDetail = seSet.Split('^');
                            /*1 = AtkUP
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
                                16 = Taunt*/
                            StatusEffect tempSE;
                            //set SE according to ID
                            switch (int.Parse(seDetail[0]))
                            {
                                case 1:
                                    tempSE = new Ef_AtkUP();
                                    break;
                                case 2:
                                    tempSE = new Ef_SpdUP();
                                    break;
                                case 3:
                                    tempSE = new Ef_DefUP();
                                    break;
                                case 4:
                                    tempSE = new Ef_AtkDOWN();
                                    break;
                                case 5:
                                    tempSE = new Ef_SpdDOWN();
                                    break;
                                case 6:
                                    tempSE = new Ef_DefDOWN();
                                    break;
                                case 7:
                                    tempSE = new Ef_HPDOWN();
                                    break;
                                case 11:
                                    tempSE = new Ef_Poison();
                                    break;
                                case 12:
                                    tempSE = new Ef_Shield();
                                    break;
                                case 13:
                                    tempSE = new Ef_Extend();
                                    break;
                                case 14:
                                    tempSE = new Ef_Regen();
                                    break;
                                case 15:
                                    tempSE = new Ef_Stun();
                                    break;
                                case 16:
                                    tempSE = new Ef_Taunt();
                                    break;
                                default:
                                    //in case buff not found, turn to regen
                                    tempSE = new Ef_Regen();
                                    break;
                            }
                            tempSE.InitializeSE(int.Parse(seDetail[1]), int.Parse(seDetail[2]));
                            eChara.chara.statusEffectList.Add(tempSE);
                        }
                    }
                    //loads action/sequence stats
                    eChara.chara.seqIndex = int.Parse(enemyDetail[11]);
                    eChara.chara.actIndex = int.Parse(enemyDetail[12]);
                }
            }
        }
        else
        {
            //spawn new units
            int maxRange = 0;
            foreach(SpawnGroup sg in semesterList[semester-1].spawnGroup)
            {
                maxRange += sg.value;
            }
            //roll rng
            int rng = Random.Range(0,maxRange);
            int tempIndex = 0;
            while (rng > semesterList[semester - 1].spawnGroup[tempIndex].value)
            {
                rng -= semesterList[semester - 1].spawnGroup[tempIndex].value;
                tempIndex++;
            }
            foreach (EnemyChara eChara in semesterList[semester - 1].spawnGroup[tempIndex].eCharaList)
            {
                combatEngine.SpawnEnemy(eChara);
            }
            combatEngine.combatInProgress = true;
        }
        saveEnemies();
    }

    public void saveEnemies()
    {
        string saveData = "";
        foreach(GameObject go in combatEngine.ActiveEnemy)
        {
            EnemyChara eChara = go.GetComponent<EnemyChara>();
            saveData += eChara.chara.name+'#'; //index 0
            saveData += eChara.chara.GetFoeID() + "#";
            saveData += eChara.chara.HPcurr + "#";

            //saves battle data
            saveData += eChara.chara.efExtend + "#";
            saveData += eChara.chara.efPoison + "#";
            saveData += eChara.chara.efRecov + "#"; //index 5
            saveData += eChara.chara.efShield + "#";
            saveData += eChara.chara.efStunRate + "#";
            saveData += eChara.chara.efTauntRate + "#";
            if (eChara.chara.efTauntTarget != null)
            {
                saveData += eChara.chara.efTauntTarget.name + "#";
            }
            else
            {
                saveData += "none" + "#";
            }

            //saves status effect
            if (eChara.chara.statusEffectList!=null)
            {
                int seIndex = 0;
                foreach (StatusEffect se in eChara.chara.statusEffectList)
                {
                    saveData += se.StatusID + "^";
                    saveData += se.level + "^";
                    saveData += se.duration;
                    //check if the status effect is the last in the list
                    //if index+1==seList.count
                    if (seIndex + 1 < eChara.chara.statusEffectList.Count)
                    {
                        saveData += "&";
                    }
                    seIndex++;
                }
            }
            
            saveData += "#"; //index 10

            //save action in sequence
            saveData += eChara.chara.seqIndex+"#";
            saveData += eChara.chara.actIndex;
            saveData += '\n';
        }
        //Debug.Log(saveData);
        PlayerPrefs.SetString("enemyDetails",saveData);
    }
}

[System.Serializable]
public class Semester {
    public SpawnGroup[] spawnGroup;
    public Semester() { }
}

[System.Serializable]
public class SpawnGroup {
    public int value;
    public EnemyChara[] eCharaList;
    public SpawnGroup()
    {

    }
}
