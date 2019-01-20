using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Training : MonoBehaviour {

    List<int> randomPool = new List<int>();
    public PlayerLoader pl;
    List<PlayerChara> party = new List<PlayerChara>(); 
    int[] trainingId;
    int[] partyPanelId;
    int trainingActiveId = -1;
    int partyActiveId = -1;
    public Text goldText;
    public Text TrainingCost1;
    public Text TrainingCost2;
    public Text TrainingCost3;
    public int baseCost = 50;
    public int gold;
    public GameObject characterPanel1;
    public GameObject characterPanel2;
    public GameObject characterPanel3;
    public Text trainingTypes1;
    public Text trainingTypes2;
    public Text trainingTypes3;
    public string types1;
    public string types2;
    public string types3;
    public string[] Types = new string[] { "Problem Solving Training", "Public Speaking Training", "Logic Training", "Decision Making Training", "Specialized Training" };
	// Use this for initialization
	void Start () {
        //types1 = Types[Random.Range(0, Types.Length - 1)];
        //types2 = Types[Random.Range(0, Types.Length - 1)];
        //types3 = Types[Random.Range(0, Types.Length - 1)];
        //trainingTypes1.text = types1.ToString();
        //trainingTypes2.text = types2.ToString();
        //trainingTypes3.text = types3.ToString();
        gold = PlayerPrefs.GetInt("Player Gold");
        
        //party = pl.GetParty();@@@@@@@
        randomizeTraining();
    }
	
	// Update is called once per frame
	void Update () {
        goldText.text = gold.ToString();
        PlayerPrefs.SetInt("Player Gold", gold);
	}

    void randomizeTraining()
    {
        randomPool.Clear();
        trainingId = new int[3];
        for(int i = 0; i < 5; i++)
        {
            randomPool.Add(i);
        }
        for(int i = 0; i < 3; i++)
        {
            int rng = Random.Range(0, randomPool.Count);
            trainingId[i] = randomPool[rng];
            randomPool.RemoveAt(rng);
        }
        trainingTypes1.text = Types[trainingId[0]];
        trainingTypes2.text = Types[trainingId[1]];
        trainingTypes3.text = Types[trainingId[2]];

    }

    public void setActiveTrainingId(int id)
    {
        if (trainingActiveId == trainingId[id])
        {
            trainingActiveId = -1;
        }else
        {
            trainingActiveId = trainingId[id];
        }
    }

    public void characterActiveId(int choosenId)
    {
        if (partyActiveId == choosenId)
        {
            partyActiveId = -1;
        }else
        {
            partyActiveId = choosenId;
        }
    }

    public void trainingCost()
    {
        //baseCost;

    }

    public void confirmButton()
    {
        //cek gold

        //lanjut ke training
        switch (trainingActiveId)
        {
            //case 0: party[partyActiveId].chara.HPmax += rng;
            //    party[partyActiveId].trainCount++;
            //    break;
        }

        //save gold & characters
        //PlayerPrefs.SetInt();
        //pl.PlayerCharaSave(0,party[partyActiveId]);
    }
}
