using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolTracker", menuName = "SO/BoolTracker")]
public class BooleanTracker : ScriptableObject
{
    public bool[] bools;

    public void SetBools(int index)
    {
        bools[index] = true;
    }
}
