using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Builder : MonoBehaviour
{
    public UnityEvent OnBuildModeEnter;
    public UnityEvent<Building> OnInspectorModeEnter;
    public UnityEvent OnExitSelection;


    float castRadius = .6f;
    BuildPort selectBuildPort;
    Building selectBuilding;

    public SoundSO buildSound;
    public SoundSO selectSound;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.PlaySound(selectSound);
            //print("Searching");
            //RaycastHit2D[] hits = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), castRadius, Vector3.zero, 0);
            Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), castRadius);
            foreach (Collider2D hit in hits)
            {
                print(hit.transform);
                if (hit.transform.TryGetComponent<BuildPort>(out BuildPort bp))
                {
                    if (bp.building == null)
                    {
                        selectBuildPort = bp;
                        OnBuildModeEnter.Invoke();
                        DeselectBuilding();
                        break;
                    }
                    else
                    {
                        SelectBuilding(bp.building);
                        return;
                    }
                    
                }

                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnExitSelection?.Invoke();
            DeselectBuilding();
        }
    }

    // Called by Listener On Build Panel UI
    public void Build(Building building)
    {
        if (selectBuildPort == null)
        {
            print("Select Build Port is Null");
        }
        else
        {
            if (Bank.instance.TransactMaterial(-building.buildCost))
            {
                selectBuildPort.Build(building);
                OnExitSelection?.Invoke();
                SelectBuilding(selectBuildPort.building);
                selectBuildPort = null;

                SoundManager.instance.PlaySound(buildSound);
            }
        }
    }
    private void SelectBuilding(Building b)
    {
        if (selectBuilding != null) DeselectBuilding();
        OnInspectorModeEnter?.Invoke(b);
        selectBuilding = b;
        selectBuilding.Select(true);

        
    }
    private void DeselectBuilding()
    {
        selectBuilding?.Select(false);
        selectBuilding = null;
    }
}
