using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveManager : MonoBehaviour {

    public static saveManager Instance { set; get; }
    public saveState state;

    private void Awake()
    {
        resetSave();
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

        Debug.Log(Helper.Serialize<saveState>(state));
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<saveState>(state));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<saveState>(PlayerPrefs.GetString("save"));
        }else
        {
            state = new saveState();
            Save();
            Debug.Log("No save file found!");
        }
    }

    public bool IsCharacterOwned(int index)
    {
        return (state.characterOwned & (1 << index)) != 0;
    }

    public bool BuyCharacter(int index, int cost)
    {
        if(state.points >= cost)
        {
            state.points -= cost;
            unlockCharacter(index);

            Save();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void unlockCharacter(int index)
    {
        state.characterOwned |= 1 << index;
    }

    public void resetSave()
    {
        PlayerPrefs.DeleteKey("save");
    }
}
