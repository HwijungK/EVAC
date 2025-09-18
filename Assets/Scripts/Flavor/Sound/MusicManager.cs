using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip music;
    public AudioSource[] audioSources;
    int sourceIndex = 0;

    public float fadeTime;
    float startVolumeChange;
    float endVolumeChange;
    bool mute = true;

    public double goalTime;
    private void Start()
    {
        foreach (AudioSource source in audioSources)
        {
            source.volume = mute ? 0 : 1;
        }
    }

    private void Update()
    {
        
        if (Time.time > goalTime)
        {
            PlayScheduledClip();
        }
        if (Time.time < endVolumeChange)
        {
            float start = mute ? 1.0f : 0.0f;
            float end = 1 - start;
            foreach (AudioSource source in audioSources)
            {
                source.volume = Mathf.Lerp(start, end, (Time.time - startVolumeChange) / (endVolumeChange - startVolumeChange));
            }
        }
    }

    /*private void OnPlayMusic()
    {
        goalTime = AudioSettings.dspTime + music.length;

        audioSource.clip = music;
        audioSource.PlayScheduled(goalTime);

        goalTime = goalTime + (double)music.samples / music.frequency;
    }*/

    private void PlayScheduledClip()
    {
        audioSources[sourceIndex].clip = music;
        audioSources[sourceIndex].PlayScheduled(goalTime);

        double musicDuration = (double)music.samples / music.frequency;
        goalTime += musicDuration - 4;
        sourceIndex = 1 - sourceIndex;
    }
    
    public void Mute(bool mute)
    {
        if (this.mute != mute)
        {
            this.mute = mute;
            startVolumeChange = Time.time;
            endVolumeChange = Time.time + fadeTime;
        }
        
    }
}
