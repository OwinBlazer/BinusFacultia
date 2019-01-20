using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMHandler : MonoBehaviour {
    public static BGMHandler BGM;
    AudioSource source;
	// Use this for initialization
	void Awake () {
        if (BGM == null)
        {
            BGM = this;
            source = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

	}
	
	public void RequestBGM(AudioClip clip) {
        source.loop = true;
        if (!source.isPlaying)
        {
            source.clip = clip;
            source.Play();
        }
        else
        {
            if (source.clip != clip)
            {
                source.clip = clip;
                source.Play();
            }
        }
        

    }
}
