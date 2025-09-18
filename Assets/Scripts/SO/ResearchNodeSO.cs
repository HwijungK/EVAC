using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ResearchNodeSO", menuName = "SO/Research/ResearchNode")]
public class ResearchNodeSO : ScriptableObject
{
    public string description;


    public bool activated;
    public int unlockCost = 200;
    public bool unlockedInGame;
    //public ResearchNodeSO[] children;
}
