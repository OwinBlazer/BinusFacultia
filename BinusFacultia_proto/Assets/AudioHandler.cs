using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {
    public static AudioHandler audioHandler;
    List<AudioSource> allAudioSource;
    Queue<AudioSource> audiosource=new Queue<AudioSource>();
    Queue<AudioSource> inUse = new Queue<AudioSource>();
    float setVolume;
    // Use this for initialization
    void Awake() {
        allAudioSource = new List<AudioSource>();
        if (audioHandler == null)
        {
            audioHandler = this;
            DontDestroyOnLoad(this);

            for (int i = 0; i < transform.childCount; i++)
            {
                AudioSource tempSource = transform.GetChild(i).GetComponent<AudioSource>();
                allAudioSource.Add(tempSource);
                audiosource.Enqueue(tempSource);
            }
            setVolume = allAudioSource[0].volume;
        }
        else
        {
            Destroy(this);
        }
	}
	
	public void playSFX(AudioClip clip)
    {
        if (audiosource.Count > 0)
        {
            foreach(AudioSource adSrc in allAudioSource)
            {
                adSrc.volume = (setVolume/3)+((2/3)/((float)inUse.Count+1));
            }
            AudioSource source = audiosource.Dequeue();

            inUse.Enqueue(source);
            source.clip = clip;
            source.pitch = Random.Range(0.8f, 1.2f);
            source.Play();
            StartCoroutine(returnSource(clip.length));
        }
    }
    IEnumerator returnSource(float waitLength)
    {
        yield return new WaitForSeconds(waitLength);
        audiosource.Enqueue(inUse.Dequeue());
    }
}
