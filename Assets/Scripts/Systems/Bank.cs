using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bank : MonoBehaviour
{
    public int startingMaterial = 200;
    int material;

    public UnityEvent<int> MaterialAmountChanged;

    public static Bank instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;

        material = 0;
        TransactMaterial(startingMaterial);
    }
    public bool CanTransact(int amount)
    {
        if (material + amount < 0) return false;
        else
        {
            return true;
        }
    }
    public bool TransactMaterial(int amount)
    {
        if (material + amount < 0) return false;
        else
        {
            material += amount;
            //print(amount);
            MaterialAmountChanged?.Invoke(material);
            return true;
        }
    }
}
