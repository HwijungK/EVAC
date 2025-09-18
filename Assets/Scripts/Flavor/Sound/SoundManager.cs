 using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    public MusicManager[] musicManagers;
    
    int musicIndex = 0;

    private void Awake()
    {
        //print("Awake");
        DontDestroyOnLoad(gameObject);
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            //SetMusicIndex(0);
        }
        
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            IncreaseMusicIndex();
        }*/
    }

    public void PlaySound(SoundSO soundSO)
    {
        audioSource.PlayOneShot(soundSO.clip, soundSO.volume);
    }

    public void IncreaseMusicIndex()
    {
        //print("Old Index was: " + musicIndex + " new index is" + musicIndex + 1);
        musicIndex++;
        musicIndex = Mathf.Min(musicIndex, musicManagers.Length - 1);
        musicManagers[musicIndex].Mute(false);
    }
    public void SetMusicIndex(int index)
    {
        musicIndex = index;
        for (int i  =0; i < musicManagers.Length; i++)
        {
            if (i <= index)
            {
                musicManagers[i].Mute(false);
            }
            else
            {
                musicManagers[i].Mute(true);
            }
        }
    }
}
