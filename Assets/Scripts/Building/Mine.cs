using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    public float productionCycle = 1;
    public int materialMined = 10;

    float lastProductionTime;
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if (lastProductionTime + productionCycle <= Time.time)
        {
            lastProductionTime = Time.time;
            Bank.instance.TransactMaterial(materialMined);

            // Do A cool particle :)
        }
    }
    public override void SetUpgradeStats()
    {
        base.SetUpgradeStats();
        materialMined = buildingUpgradeSO.data[level - 1].intData[1];
    }
}
