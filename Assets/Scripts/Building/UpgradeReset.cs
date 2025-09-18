using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeReset : MonoBehaviour
{
    public ResearchSO perRound;
    //public ResearchSO perGame;

    public void Awake()
    {
        perRound.ResetRound();
    }
}
