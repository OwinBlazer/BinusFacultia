using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenu : MonoBehaviour {

	public void LoadSceneNamed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void exitApp()
    {
        Application.Quit();
    }
}
