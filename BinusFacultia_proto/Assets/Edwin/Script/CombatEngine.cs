﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActEntryList {
    public int entryID;
    public Action action;
    public ActEntryList(Action act, int entryID)
    {
        this.action = act;
        this.entryID = entryID;
    }
}

public class CombatEngine : MonoBehaviour {
    //tempo
    public GameObject enemy1, enemy2, enemy3;
    //more final
    [SerializeField] public GameObject[] EnemyList;
    [SerializeField] public List<GameObject> ActiveEnemy;

    [SerializeField] GameObject targetListParent;
    [SerializeField] GameObject targetButton;
    private List<GameObject> buttonList = new List<GameObject>();
    [SerializeField] Animator anim;
    [SerializeField] private GameObject executeButton;
    [SerializeField]Chara tempSource;
    
    PlayerAction tempAction;
    Chara tempTarget;
    private int phaseID;
    //0=start
    //1=order
    //2=execution
    [SerializeField] public Text logbox;
    [SerializeField] public Text[] charNameText;
    [SerializeField] public Text[] charHPText;
    [SerializeField] public Text[] charAPtext;
    public PlayerLoader playerLoader;
    List<Chara> allChara = new List<Chara>();
    List<ActEntryList> allActions = new List<ActEntryList>();
    public GameObject[] skillButton;
    public GameObject[] itemButton;
    public bool combatInProgress;

    public List<Chara> GetAllChara()
    {
        return allChara;
    }
	// Use this for initialization
	void Start () {
        phaseID = 0;

        playerLoader.initializeParty();
        List<Chara> tempList = playerLoader.GetPlayerCharacters();
        foreach(Chara chara in tempList)
        {
            allChara.Add(chara);
        }
        for(int i = 0; i < 5; i++)
        {
            ActiveEnemy.Add(transform.GetChild(i).gameObject);
        }
        /*

        allChara.Add(new Chara("Gatto", 30, 20, 10, 3, 9, false));
        allChara.Add(new Chara("Gitte", 30, 20, 10, 3, 8, false));
        allChara.Add(new Chara("Goando", 30, 20, 10, 3, 10, false));
        */

        LoadEnemy();
        combatInProgress = true;
        UpdateHPUI();
        Combat();
    }
    private void LoadEnemy()
    {
        //FIX THIS @
        //This section loads all listed enemy in the encounter list
        SpawnEnemy(0);
        SpawnEnemy(0);
        SpawnEnemy(1);
    }
    public EnemyChara SpawnEnemy(EnemyChara spawnChara)
    {
        //another way to spawn enemy
        foreach (GameObject go in ActiveEnemy)
        {
            EnemyChara eChara = go.GetComponent<EnemyChara>();
            //if dead/empty
            if (eChara.chara.HPcurr <= 0)
            {
                //initialize that component as the correct spawn

                eChara.enemyID = spawnChara.enemyID;
                eChara.chara.isEnemy = true;

                eChara.chara.baseHPmax = spawnChara.chara.baseHPmax;
                eChara.chara.HPmax = spawnChara.chara.HPmax;
                eChara.chara.HPcurr = spawnChara.chara.HPcurr;

                eChara.chara.MPmax = spawnChara.chara.MPmax;
                eChara.chara.MPcurr = spawnChara.chara.MPcurr;

                eChara.chara.name = spawnChara.chara.name;

                eChara.chara.atk = spawnChara.chara.atk;
                eChara.chara.baseAtk = spawnChara.chara.baseAtk;

                eChara.chara.spd = spawnChara.chara.spd;
                eChara.chara.baseSpd = spawnChara.chara.baseSpd;

                eChara.chara.def = spawnChara.chara.def;
                eChara.chara.baseDef = spawnChara.chara.baseDef;
                eChara.chara.sequence = spawnChara.chara.sequence;
                eChara.chara.Initialize();
                allChara.Add(eChara.chara);
                UpdateHPUI();
                return eChara;
            }
        }
        return null;
    }
    public EnemyChara SpawnEnemy(int ID)
    {
        //add enemy here :D 
        //Step 1, assign an idle ActiveEnemy object
        //Step 2, get the object's EnemyChara component
        //Step 3, copy the enemyChara component values
        //step 4, initialize the new enemy
        foreach (GameObject go in ActiveEnemy)
        {
            EnemyChara eChara = go.GetComponent<EnemyChara>();
            //if dead/empty
            if (eChara.chara.HPcurr <= 0)
            {
                EnemyChara sourceChara = EnemyList[ID].GetComponent<EnemyChara>();
                //initialize that component as the correct spawn
                
                eChara.enemyID = ID;
                eChara.chara.isEnemy = true;

                eChara.chara.baseHPmax = sourceChara.chara.baseHPmax;
                eChara.chara.HPmax = sourceChara.chara.HPmax;
                eChara.chara.HPcurr = sourceChara.chara.HPcurr;

                eChara.chara.MPmax = sourceChara.chara.MPmax;
                eChara.chara.MPcurr = sourceChara.chara.MPcurr;

                eChara.chara.name = sourceChara.chara.name;

                eChara.chara.atk = sourceChara.chara.atk;
                eChara.chara.baseAtk = sourceChara.chara.baseAtk;

                eChara.chara.spd = sourceChara.chara.spd;
                eChara.chara.baseSpd = sourceChara.chara.baseSpd;

                eChara.chara.def = sourceChara.chara.def;
                eChara.chara.baseDef = sourceChara.chara.baseDef;
                eChara.chara.sequence = sourceChara.chara.sequence;
                eChara.chara.Initialize();
                allChara.Add(eChara.chara);
                UpdateHPUI();
                return eChara;
            }
        }
        return null;
    }
    public void Combat()
    {
        if (combatInProgress){
            StartPhase();
            OrderPhase();
            //execution phase is triggered by execute button
        }
    }
    void StartPhase()
    {
        foreach(Chara chara in allChara)
        {
            if (chara.actionPointMax < 3)
            {
                chara.actionPointMax++;
            }
        }
        EffectPoison();
        UpdateHPUI();
        RunAllEffect();
        UpdateAPUI();

    }
    void EffectPoison()
    {
        foreach (Chara chara in allChara)
        {
            if (chara.efPoison != 0)
            {
                Debug.Log("Poisoned here!");
                chara.HPcurr -= chara.efPoison;
                logbox.text += chara.name+" suffers "+chara.efPoison+" Overload damage!\n";
            }
        }
    }
    void RunAllEffect()
    {
        foreach(Chara chara in allChara)
        {
            int indexFlag = 0;
            while (indexFlag < chara.statusEffectList.Count)
            {
                //chara.statusEffectList[indexFlag].RunEffect(chara);
                chara.statusEffectList[indexFlag].ReduceDur(chara);
                if (chara.statusEffectList[indexFlag].GetDur() <= 0)
                {
                    chara.statusEffectList[indexFlag].ResetEffect(chara);
                    chara.statusEffectList.RemoveAt(indexFlag);
                }
                else
                {
                    indexFlag++;
                }
            }
        }
    }
    void OrderPhase()
    {
        phaseID = 1;
        allActions.Clear();
        foreach(Chara chara in allChara)
        {
            if (chara.HPcurr > 0)
            {
                if (chara.isEnemy)
                {
                    chara.
                        queuedAction.Add(
                        chara.GetNextAction(allChara));
                }
            }
        }

    }
    public void SetSource(int chosenID)
    {
        if (allChara[chosenID].HPcurr > 0 && combatInProgress)
        {

            //Note, this line only works if the characters are the ones loaded in FIRST
            tempSource = allChara[chosenID];
            //tempSource = sourceChara;
            anim.SetInteger("actionMenuID", 1);
        }
    }
    public void SetActionDefend()
    {
        tempSource.queuedAction.Clear();
        UpdateAPUI();
        anim.SetInteger("actionMenuID", 0);
    }
    public void SetAction(int actionID)
    {
        
        switch (actionID)
        {
            case 0://attack
                ActionAttack();
                break;
            case 1://skill
                ActionSkill();
                break;//open skill menu
            case 2://item
                ActionItem();
                break;//open items menu
        }

    }
    private void ActionItem()
    {
        LoadValidItems();
        anim.SetInteger("actionMenuID", 4);
    }
    private void LoadValidItems()
    {
        int itemCount = 0;
        List<ItemInterface> validItems = playerLoader.GetItems();
        for(int i = 0; i < validItems.Count; i++)
        {
            if (validItems[i].GetQty() > 0)
            {
                itemCount++;
                itemButton[i].SetActive(true);
                itemButton[i].transform.GetChild(0).GetComponent<Text>().text = validItems[i].GetName();
            }
            else
            {
                itemButton[i].SetActive(false);
            }
        }
        if (itemCount <= 0)
        {
            logbox.text = "You have no usable items!";
            anim.SetInteger("actionMenuID", 0);
        }
    }
    public void SetItem(int itemID)
    {
        tempAction = playerLoader.allItem[itemID].UseItem(tempSource);
        if (tempAction.needTarget)
        {
            LoadValidTargets();
        }
        else
        {
            loadActionToChara();
        }
    }
    private void ActionSkill()
    {
        LoadValidSkills();
        anim.SetInteger("actionMenuID", 3);
    }
    private void LoadValidSkills()
    {
        //based on character's skill
        List<SkillInterface> validSkill = playerLoader.GetSkills(tempSource);
        for(int i = 0; i < validSkill.Count; i++)
        {
            if (validSkill[i].skillLevel > 0)
            {
                skillButton[i].SetActive(true);
                skillButton[i].transform.GetChild(0).GetComponent<Text>().text = validSkill[i].skillName;
            }
        }
    }
    private void ActionAttack()
    {
        tempAction = new paAttack();
        tempAction.needTarget = true;
        tempAction.source = tempSource;
        LoadValidTargets();
    }
    public void LoadValidTargets()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            Destroy(buttonList[i]);
        }
        buttonList.Clear();
        //based on action's requirement
        List<Chara> validTarget = tempAction.GetValidTarget(allChara);
        foreach (Chara chara in validTarget)
        {
            //use object pooling? <@
            GameObject newButton = Instantiate(targetButton, targetListParent.transform);
            buttonList.Add(newButton);
            newButton.GetComponent<TargetButton>().initiateButton(chara.name, allChara.IndexOf(chara));
        }
        anim.SetInteger("actionMenuID", 2);
    }
    public void SetSkill(int skillID)
    {
        // skillID loads the skill depending on the chosen character's skill on that slot
        tempAction = playerLoader.party[allChara.IndexOf(tempSource)].skillList[skillID].GetSkill(tempSource,allChara);
        if (tempAction.needTarget)
        {
            LoadValidTargets();
        }
        else
        {
            loadActionToChara();
        }
        // supply target into action
    }
    public void SetTarget(int slotID)
    {
        tempAction.target = allChara[slotID];
        //load name into text
        loadActionToChara();
    }
    private void loadActionToChara()
    {
        //safety net
        if (tempAction != null)
        {
            if (tempAction.source.queuedAction.Count < tempAction.source.actionPointMax)
            {
                tempAction.source.queuedAction.Add(tempAction);
                UpdateAPUI();
            }
            else
            {
                logbox.text = "";
                logbox.text = tempAction.source.name+" can't do any more actions!";
            }
            anim.SetInteger("actionMenuID", 0);
        }
        tempAction = null;
    }
    public void startExecutionPhase()
    {
        if (combatInProgress)
        {
            List<Chara> charaWithoutAction = new List<Chara>();

            //all chara with no action -> defending
            foreach (Chara chara in allChara)
            {
                if (chara.HPcurr > 0 && !chara.isEnemy)
                {
                    if (chara.queuedAction.Count == 0)
                    {
                        chara.isDefending = true;
                    }
                    else
                    {
                        chara.isDefending = false;
                    }
                }
            }
            foreach (Chara chara in allChara)
            {
                foreach(Action act in chara.queuedAction)
                {
                    allActions.Add(new ActEntryList(act,allActions.Count));
                    Debug.Log(chara.name+" with id "+(allActions.Count-1).ToString() +"and speed "+ chara.spd.ToString());
                }
            }
            ExecutionPhase();
        }
        
    }
    void ExecutionPhase()
    {
        logbox.text = "";
        while (allActions.Count > 0)
        {
            allActions.Sort(new actionSpdComp());

            //execution by speed batch here!
            ExecuteByBatch();
            UpdateActionList();
            UpdateHPUI();
            VictoryCheck();
        }
        EndPhase();
    }
    void EndPhase()
    {
        allActions.Clear();
        foreach(Chara chara in allChara)
        {
            chara.queuedAction.Clear();
            chara.isDefending = false;
        }
        Combat();
    }
    void UpdateHPUI()
    {
        for(int i = 0; i < allChara.Count; i++)
        {
            charNameText[i].text = allChara[i].name;
            charHPText[i].text = allChara[i].HPcurr.ToString();
        }
    }
    private void UpdateAPUI()
    {
        for(int i = 0; i < 3; i++)
        {
            charAPtext[i].text = (allChara[i].actionPointMax - allChara[i].queuedAction.Count).ToString();
        }
    }
    void ExecuteByBatch()
    {
        int flagSpd = allActions[0].action.source.spd;
        while (allActions.Count>0&&flagSpd == allActions[0].action.source.spd)
        {
            if (EffectStun(allActions[0].action.source.efStunRate))
            {
                //if stun succeeds
                logbox.text += allActions[0].action.source.name + " is stunned for this action!\n";
            }
            else
            {
                if (allActions[0].action.source.HPcurr > 0)
                {
                    allActions[0].action.executeAction();
                    allActions[0].action.updateLog(logbox);
                    allActions[0].action.source.actionPointMax--;
                }
            }
            allActions.RemoveAt(0);
        }
    }
    bool EffectStun(float rate)
    {
        float failNum = 1 - rate;
        float rng = Random.Range((float)0, (float)1);
        if (rng <= failNum)
        {
            //stun fails
            return false;
        }
        else
        {
            //stun succeeds
            return true;
        }
    }
    void UpdateActionList()
    {
        int tempFlag = 0;
        while (tempFlag < allActions.Count)
        {
            if (allActions[tempFlag].action.source.HPcurr <= 0)
            {
                allActions.RemoveAt(tempFlag);
            }
            else
            {
                tempFlag++;
            }
        }
    }
    void VictoryCheck()
    {
        int allyCount=0;
        int enemyCount=0;

        int tempIndex = 0;
        while (tempIndex < allChara.Count)
        {
            Chara chara = allChara[tempIndex];
            if (chara.isEnemy)
            {
                if (chara.HPcurr > 0)
                {
                    enemyCount++;
                }
                else
                {
                    allChara.RemoveAt(tempIndex);
                    break;
                }
            }
            else
            {
                if (chara.HPcurr > 0)
                {
                    allyCount++;
                }
            }
            tempIndex++;
        }

        if (enemyCount == 0)
        {
            BattleVictory();
        }
        if (allyCount == 0)
        {
            BattleLost();
        }
    }
    void BattleVictory()
    {
        allActions.Clear();
        combatInProgress = false;
        //continue to next wave here<======================@
        logbox.text += "you won!";
        executeButton.SetActive(false);
        anim.SetInteger("actionMenuID",9);

    }
    void BattleLost()
    {
        combatInProgress = false;
        logbox.text += "you lost...!";
        executeButton.SetActive(false);
        //battle lost trigger here<===========================@
        anim.SetInteger("actionMenuID", 9);
    }
}
class actionSpdComp : IComparer<ActEntryList>
{
    public int Compare(ActEntryList act1, ActEntryList act2)
    {
        if (act1.action.source.spd > act2.action.source.spd)
        {
            return -1;
        }else
        if (act1.action.source.spd < act2.action.source.spd)
        {
            return 1;
        }
        else if(act1.entryID > act2.entryID)
        {
            return 1; }
        else if(act1.entryID < act2.entryID){ return -1; }
        return 0;
    }
}
