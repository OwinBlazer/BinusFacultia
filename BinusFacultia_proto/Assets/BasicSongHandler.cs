using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSongHandler : MonoBehaviour {
    [SerializeField] AudioClip BGM;
	// Use this for initialization
	void Start () {
        BGMHandler.BGM.RequestBGM(BGM);
	}
}
