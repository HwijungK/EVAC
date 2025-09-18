using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
public class ResearchLabInspectorUI : InspectorUI
{
    public ResearchSO researchSO;
    public TextMeshProUGUI[] costTexts;
    public ResearchNodeSO[] researchInOrder;

    public SoundSO researchSound;

    public void UnlockResearch(ResearchNodeSO node)
    {
        if (Bank.instance.CanTransact(-node.unlockCost))
        {
            if (researchSO.ActivateResearchNode(node.name))
            {
                Bank.instance.TransactMaterial(-node.unlockCost);
                SoundManager.instance.PlaySound(researchSound);

            }
        }
    }
    public override void Start()
    {
        base.Start();
        for (int i = 0; i < costTexts.Length; i++)
        {
            costTexts[i].text = researchInOrder[i].unlockCost.ToString();
        }
    }

}
