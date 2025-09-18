using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soundSO", menuName = "SO/Sound")]
public class SoundSO : ScriptableObject
{
    public AudioClip clip;
    [Range(0, 1f)]
    public float volume;

}
