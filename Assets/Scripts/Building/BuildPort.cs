using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPort : MonoBehaviour
{
    [HideInInspector] public Building building;
    public float buildingYDistance = 0.35f;
    public void Build(Building building)
    {
        this.building = Instantiate(building, transform);
        
        building.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        building.transform.localPosition = Vector3.up * buildingYDistance;
        print("builing local position" + building.transform.localPosition);
    }
}
