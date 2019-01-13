using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkDataCheck : MonoBehaviour {
    [SerializeField] string[] sceneNames;
    [SerializeField] Animator anim;
    [SerializeField] mainmenu sceneLoader;
	// Use this for initialization
	public void ContinueSession()
    {
        //continue session
        int bookmark = PlayerPrefs.GetInt("sessionBookmark", 0);
        sceneLoader.LoadSceneNamed(sceneNames[bookmark]);
    }

    public void NewSession()
    {
        //new session
        sceneLoader.LoadSceneNamed(sceneNames[0]);
    }

    public void CancelSelect()
    {
        anim.SetBool("ContinuePrompt", false);
    }

    public void SessionCheck()
    {
        //open prompt
        int bookmark = PlayerPrefs.GetInt("sessionBookmark", 0);
        if (bookmark == 0)
        {
            sceneLoader.LoadSceneNamed(sceneNames[bookmark]);
        }
        else
        {
            anim.SetBool("ContinuePrompt",true);
        }
    }



}
