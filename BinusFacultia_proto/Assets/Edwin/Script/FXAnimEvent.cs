using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXAnimEvent : MonoBehaviour {
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void FXHitFinish()
    {
        anim.SetBool("FXHitPlay", false);
    }
    public void FXHealFinish()
    {
        anim.SetBool("FXHealPlay", false);
    }
    public void FXBuffFinish()
    {
        anim.SetBool("FXBuffPlay", false);
    }
    public void FXDebuffFinish()
    {
        anim.SetBool("FXDebuffPlay", false);
    }
}
