using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvacCounter : MonoBehaviour
{
    public static EvacCounter instance;
    public UnityEvent<int> evactuated;

    public int neededEvac;
    int evacCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        evactuated?.Invoke(0);
    }
    public void IncreaseEvacCount()
    {
        evacCount++;
        evactuated?.Invoke(evacCount);
        if (evacCount >= neededEvac)
        {
            // End Game
            FindObjectOfType<UIController>().EndGame(true);
            print("YOUWINFYAYAYA");
        }
    }
    
}
