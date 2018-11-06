using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CombatEngine : MonoBehaviour {
    //tempo
    public GameObject enemy1, enemy2, enemy3;
    [SerializeField] GameObject targetListParent;
    [SerializeField] GameObject targetButton;
    private List<GameObject> buttonList = new List<GameObject>();
    [SerializeField] Animator anim;
    [SerializeField] private GameObject executeButton;
    Chara tempSource;
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

    List<Chara> allChara = new List<Chara>();
    List<Action> allActions = new List<Action>();
    bool combatInProgress;

    public List<Chara> GetAllChara()
    {
        return allChara;
    }
	// Use this for initialization
	void Start () {
        enemy1 = Instantiate(enemy1);
        enemy2 = Instantiate(enemy1);
        enemy3 = Instantiate(enemy1);
        phaseID = 0;
        allChara.Add(new Chara("Gatto", 30, 20, 10, 3, 9, false));
        allChara.Add(new Chara("Gitte", 30, 20, 10, 3, 8, false));
        allChara.Add(new Chara("Goando", 30, 20, 10, 3, 10, false));
        allChara.Add(enemy1.GetComponent<EnemyChara>().chara);
        allChara[allChara.Count - 1].Initialize();
        allChara.Add(enemy2.GetComponent<EnemyChara>().chara);
        allChara[allChara.Count - 1].Initialize();
        allChara.Add(enemy3.GetComponent<EnemyChara>().chara);
        allChara[allChara.Count - 1].Initialize();
        combatInProgress = true;
        UpdateHPUI();
        Combat();
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
        UpdateAPUI();
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
                    Debug.Log(chara.name);
                    Debug.Log(chara.queuedAction.Count);
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
                tempAction = new paAttack();
                tempAction.needTarget = true;
                break;
            case 1://skill@
                break;//open skill menu
            case 2://item@
                break;//open items menu
        }
        tempAction.source = tempSource;
        if (!tempAction.needTarget)
        {
            loadActionToChara();
        }
        else
        {
            //OPEN TARGET MENU
            LoadValidTargets();
            anim.SetInteger("actionMenuID", 2);
        }

    }
    public void LoadValidTargets()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            Destroy(buttonList[i]);
        }
        buttonList.Clear();
        foreach (Chara chara in allChara)
        {
            //based on action's requirement<================================================@
            if(chara.HPcurr > 0&&chara.isEnemy)
            {
                GameObject newButton = Instantiate(targetButton, targetListParent.transform);
                buttonList.Add(newButton);
                newButton.GetComponent<TargetButton>().initiateButton(chara.name, allChara.IndexOf(chara));
            }
        }
    }
    public void SetSkill(int skillID)
    {
        //skillID loads the skill depending on the chose character's skill on that slot@
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
                        Debug.Log("Defending: " + chara.name);
                    }
                    else
                    {
                        chara.isDefending = false;
                    }
                }
            }

            foreach (Chara chara in allChara)
            {
                foreach (Action action in chara.queuedAction)
                {
                    allActions.Add(action);
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
        //FIX THIS FOR FINAL GAME<=======================================@
        allActions[0].executeAction();
        allActions[0].updateLog(logbox);
        allActions[0].source.actionPointMax--;
        allActions.RemoveAt(0);
    }
    void UpdateActionList()
    {
        int tempFlag = 0;
        while (tempFlag < allActions.Count)
        {
            if (allActions[tempFlag].source.HPcurr <= 0)
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
        foreach(Chara chara in allChara)
        {
            if (chara.isEnemy)
            {
                if (chara.HPcurr > 0)
                {
                    enemyCount++;
                }
            }
            else
            {
                if (chara.HPcurr > 0)
                {
                    allyCount++;
                }
            }
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
class actionSpdComp : IComparer<Action> {
    public int Compare(Action act1, Action act2)
    {
        if (act1.source.spd > act2.source.spd)
        {
            return -1;
        }
        if (act1.source.spd < act2.source.spd)
        {
            return 1;
        }
        return 0;
    }
}
