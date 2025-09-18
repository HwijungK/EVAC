using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public void UpMusic()
    {
        SoundManager.instance.IncreaseMusicIndex();
    }
}
