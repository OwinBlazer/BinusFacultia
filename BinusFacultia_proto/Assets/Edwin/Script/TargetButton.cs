using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetButton : MonoBehaviour {
    public int slotID;
    public CombatEngine main;
    public Text text;
    private void Start()
    {
        main = FindObjectOfType<CombatEngine>();
    }
    private void callNewOnClick()
    {
        main.SetTarget(slotID);
    }
    public void initiateButton(string name, int slotID)
    {
        text.text = name;
        this.slotID = slotID;
        this.GetComponent<Button>().onClick.AddListener(callNewOnClick);
    }
}
