using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MineInspectorUI : InspectorUI
{
    
    /*[Header("Static Text")]
    TextMeshProUGUI titleTMP;
    TextMeshProUGUI descriptionTMP;*/

    [Header("DynamicOnUpgradeText")]
    
    public TextMeshProUGUI produceRateTMP;

    //[Header("DynamicText")]


    

    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
        //levelTMP.text = "Level 1";
        //print(building as Mine);
    }
    public override void Update()
    {
        base.Update();
    }
    public override void UpdateUIOnUpgrade()
    {
        base.UpdateUIOnUpgrade();
        if (building != null)
        {
            produceRateTMP.text = (building as Mine).materialMined / (building as Mine).productionCycle + "/sec";
        }
    }

}

