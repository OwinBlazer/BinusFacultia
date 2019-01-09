using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chara
{
    public List<StatusEffect> statusEffectList = new List<StatusEffect>();
    public List<Action> queuedAction = new List<Action>();
    public bool isDefending;
    public string name;
    public int HPmax;
    public int HPcurr;
    public int MPmax;
    public int MPcurr;
    public int atk;
    public int def;
    public int spd;
    public int baseHPmax;
    public int baseMPmax;
    public int baseAtk;
    public int baseDef;
    public int baseSpd;

    public int efShield;
    public Chara efTauntTarget;
    public float efTauntRate;
    public float efStunRate;
    public int efRecov;
    public int efExtend;
    public int efPoison;

    public int actionPointMax;
    public bool isEnemy;
    private int enemyID;

    public Chara(string name, int HPmax, int MP, int atk, int def, int spd, bool isEnemy)
    {
        this.name = name;
        this.HPmax = HPmax;
        HPcurr = HPmax;
        this.MPmax = MP;
        MPcurr = MP;
        this.atk = atk;
        this.def = def;
        this.spd = spd;
        baseDef = def;
        baseSpd = spd;
        baseAtk = atk;
        this.isEnemy = isEnemy;
        isDefending = false;
        actionPointMax = 0;
        queuedAction = new List<Action>();
    }
    public void InflictStatus(StatusEffect se)
    {
        //validate status effect here
        bool isNewStatusEffect = true;
        se.duration += efExtend;
        for(int i = 0; i < statusEffectList.Count; i++)
        {
            if (statusEffectList[i].StatusID == se.StatusID)
            {
                isNewStatusEffect = false;
                if (statusEffectList[i].level < se.level)
                {
                    statusEffectList[i] = se;
                }else if(statusEffectList[i].level == se.level)
                {
                    statusEffectList[i].duration = Mathf.Max(statusEffectList[i].duration, se.duration);
                }
            }
        }
        if (isNewStatusEffect)
        {
            statusEffectList.Add(se);
            se.RunEffect(this);
        }
    }
    public void Initialize()
    {
        queuedAction = new List<Action>();
        statusEffectList = new List<StatusEffect>();
        efTauntRate = 0f;
        efStunRate = 0f;
        efRecov = 0;
        efPoison = 0;
        efShield = 0;
        efExtend = 0;
        actIndex = 99;
    }
    public void TakeDamage(int damage)
    {
        if (efShield <= 0)
        {
            HPcurr -= damage;
            if (HPcurr < 0)
            {
                HPcurr = 0;
            }
        }
        else
        {
            efShield--;
            int tempFlag = 0;
            while (tempFlag < statusEffectList.Count)
            {
                if (statusEffectList[tempFlag].StatusID == 12)
                {
                    statusEffectList[tempFlag].level--;
                    if (statusEffectList[tempFlag].level <= 0)
                    {
                        statusEffectList.RemoveAt(tempFlag);
                        break;
                    }
                }
                tempFlag++;
            }
        }
    }
    public void HealDamage(int heal)
    {
        HPcurr += heal;
        if(HPcurr > HPmax)
        {
            HPcurr = HPmax;
        }
    }
    public int seqIndex=0;
    public int actIndex=9999;
    public Action GetNextAction(List<Chara> allChara)
    {
        if (actIndex >= sequence[seqIndex].actions.Length)
        {
            seqIndex = 0;
            //memilih sequence yang sesuai dengan rng
            int maxSequence = 0;
            foreach (ActionSequence seq in sequence)
            {
                maxSequence += seq.value;
            }
            int rng = Random.Range(1, maxSequence+1);
            while (rng > sequence[seqIndex].value)
            {
                rng -= sequence[seqIndex].value;
                seqIndex++;
            }
            actIndex = 0;
        }
        actIndex++;
        return sequence[seqIndex].actions[actIndex-1].GetAction(this,allChara);
    }
    public ActionSequence[] sequence;
    public void SetFoeID(int id)
    {
        enemyID = id;
    }
    public int GetFoeID()
    {
        return enemyID;
    }
}