using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
    [SerializeField] private InspectorUI inspectorPrefab;
    [HideInInspector]public InspectorUI inspector;
    [SerializeField] public BuildingUpgradeSO buildingUpgradeSO;
    public int buildCost;
    protected bool selected;
    protected bool selectedBuffer;

    public int level = 1;

    public SoundSO upgradeSound;

    // Start is called before the first frame update

    public virtual void Awake()
    {
        //print("Instantiate Inspecter");
        inspector = Instantiate(inspectorPrefab, FindObjectOfType<Canvas>().transform);
        inspector.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        inspector.building = this;
        
    }
    public virtual void Start()
    {
        
        SetUpgradeStats();
    }

    public virtual void Update()
    {
        if (selectedBuffer)
        {
            selected = true;
            selectedBuffer = false;
        }
    }

    public virtual void Select(bool select)
    {
        //selectedBuffer = select;

        if (select)
        {
            selectedBuffer = true;
        }
        else
        {
            selected = false;
        }
        //selected = select;
    }

    // returns true if upgrade was succesfull
    public virtual void Upgrade()
    {
        //print("UPgrade?");
        // intData[0] should always be Cost
        if (level < buildingUpgradeSO.data.Length && Bank.instance.TransactMaterial(-buildingUpgradeSO.data[level].intData[0])) {
            level++;
            //return true;
            SetUpgradeStats();
            inspector.Upgraded();
            SoundManager.instance.PlaySound(upgradeSound);
        }
        //return false;
    }

    // abstract - ish
    public virtual void SetUpgradeStats() {
        if (buildingUpgradeSO.data[level-1].sprites.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = buildingUpgradeSO.data[level - 1].sprites[0];
        }
    }

    private void OnDestroy()
    {
        
    }
}
