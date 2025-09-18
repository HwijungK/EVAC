using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTracker : MonoBehaviour
{
    public Material filledMaterial;
    public Material unfilledMaterial;
    public BooleanTracker tracker;
    public Image[] circles;

    public TextMeshProUGUI header1;
    public TextMeshProUGUI header2;

    private void OnEnable()
    {
        bool complete = tracker.bools[circles.Length-1];

        bool higherLevelIsComplete = false;
        for(int i = circles.Length - 1; i >= 0; i--)
        {

            if (higherLevelIsComplete)
            {

                tracker.SetBools(i);
            }

            if (tracker.bools[i])
            {
                circles[i].material = filledMaterial;
                higherLevelIsComplete = true;
                
            }
        }
        if (complete)
        {
            header1.text = "0 |-| 0 |-| THANKS FOR PLAYING0 |-| 0 |-| 0";
            header2.text = " -gnlwnd1- ";
        }

        
    }
}
