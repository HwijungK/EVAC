using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingUpgradeSO", menuName = "SO/BuildingUpgradeSO")]
public class BuildingUpgradeSO : ScriptableObject
{
    [TextArea(3,50)]
    public string key;
    public int levelCount = 1;
    public UpgradeData[] data;
}
[System.Serializable]
public class UpgradeData
{
    public Sprite[] sprites;
    public int[] intData;
    public float[] floatData;
    
}