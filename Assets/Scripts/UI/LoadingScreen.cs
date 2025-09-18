using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    Image screen;
    public AnimationClip unloadclip;
    public void Awake()
    {
        screen = GetComponent<Image>();
        screen.enabled = true;
    }
    private void Start()
    {
        
    }
    public void Unload()
    {
        GetComponent<Animator>().SetTrigger("Unload");
    }
    public float UnloadTime()
    {
        return unloadclip.length;
    }
}
