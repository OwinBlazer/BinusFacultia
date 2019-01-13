using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APBarHandler : MonoBehaviour {

    public Image[] AP=new Image[3];//debugging public
    [SerializeField] Color[] ColorModes;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 3; i++)
        {
            AP[i] =

                transform.GetChild(i).

                GetComponent<Image>();
        }
	}
    public void UpdateAP(int num)
    {
        for(int i = 0; i < 3; i++)
        {
            if (i <= num - 1)
            {
                AP[i].color =
                    ColorModes[0];
            }
            else
            {
                AP[i].color = ColorModes[1];
            }
        }
    }
}
