using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    public SoundSO sound;
    public void Play()
    {
        SoundManager.instance.PlaySound(sound);
    }
}
