using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaunchSiteInspectorUI : InspectorUI
{
    [Header("DynamicText")]
    public TextMeshProUGUI chargeTMP;
    public TextMeshProUGUI rechargeTimeTMP, maxChargesTMP;



    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        chargeTMP.text = (building as LaunchSite).projectileCharges + "/" + (building as LaunchSite).maxProjectileCharges;
    }

    public override void UpdateUIOnUpgrade()
    {
        base.UpdateUIOnUpgrade();
        rechargeTimeTMP.text = (building as LaunchSite).rechargeTime.ToString();
        maxChargesTMP.text = (building as LaunchSite).maxProjectileCharges.ToString();
    }
}
