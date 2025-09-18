using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectorUI : MonoBehaviour
{
    [HideInInspector] public Building building;
    [SerializeField] public ResearchNodeSO[] researchNodes;
    

    private ResearchNodeSO[] upgradeNodes;
    public Button upgradeButton;
    public TextMeshProUGUI upgradeCostTMP;

    [Header("DynamicOnUpgradeText")]
    public TextMeshProUGUI levelTMP;

    public virtual void Awake()
    {
        //print("Awake");
        upgradeNodes = new ResearchNodeSO[researchNodes.Length];
        for (int i = 0; i < researchNodes.Length;i++)
        {
            upgradeNodes[i] = ScriptableObject.CreateInstance<ResearchNodeSO>();
            
        }
        //upgradeNodes[0].activated = true;
    }
    public virtual void Start()
    {
        if (upgradeNodes != null)
        {
            upgradeButton.onClick.AddListener(building.Upgrade);
            upgradeButton.onClick.AddListener(UpdateUIOnUpgrade);
            UpdateUIOnUpgrade();
        }
        
    }

    public void Upgraded()
    {
        int i = 0;
        while (i < upgradeNodes.Length)
        {
            if (!upgradeNodes[i].activated)
            {
                upgradeNodes[i].activated = true;
                break;
            }
            i++; 
        }
    }

    public virtual void Update()
    {

    }

    // Update is called once per frame
    void OnDestroy()
    {
        if (upgradeButton != null)
        {
            upgradeButton.onClick.RemoveAllListeners();
        }
        
    }

    public void DestroyBuilding()
    {
        Destroy(building.gameObject);
        Destroy(gameObject);

    }
    

    public virtual void OnEnable()
    {
        //print("Enable");
        if (building != null)
        {
            UpdateUIOnUpgrade();
        }
    }
    public virtual void UpdateUIOnUpgrade()
    {
        print("UpdateUI");
        upgradeButton.interactable = (false); 
        for (int i = 0; i < researchNodes.Length; i++)
        {
            if (building.level == building.buildingUpgradeSO.data.Length)
            {
                upgradeCostTMP.text = "---";
                break;
            }
            else
            {
                print("Building Level: " + building.level);
                print("Upgrade Node Length: " + upgradeNodes.Length);
            }
            //print(upgradeNodes);
            //print(researchNodes);
            if (researchNodes[i].activated && i == researchNodes.Length - 1 && upgradeNodes[i].activated)
            {
                upgradeCostTMP.text = "---";
            }
            else if (researchNodes[i].activated)
            {
                try
                {
                    if (building != null) upgradeCostTMP.text = building.buildingUpgradeSO.data[i + 1].intData[0].ToString();
                }
                catch
                {
                    print("ERROR");
                    print("upgradeCostTMP: " + upgradeCostTMP.text);
                    print("building: " + building);
                    print("building.buildingUpgradeSO" + building.buildingUpgradeSO);
                }
            }
            else
            {
                upgradeCostTMP.text = "---";
            }

            if (!upgradeNodes[i].activated && researchNodes[i].activated)
            {
                //print(researchNodes[i].name + " has not been upgraded and has been researched");
                upgradeButton.interactable = (true); break;
            }
        }

        //print("Building: " + building);
        if (building != null)
        {
            // print("Building Level Is Now: " + building.level);
            levelTMP.text = "Level " + building.level;
        }
    }
}
