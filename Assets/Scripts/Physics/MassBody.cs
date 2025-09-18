using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassBody : MonoBehaviour
{
    public float mass = 100f;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (!PhysicsManager.instance.massBodies.Contains(this))
        {
            PhysicsManager.instance.massBodies.Add(this);
        }
    }
    private void OnDestroy()
    {
        PhysicsManager.instance.massBodies.Remove(this);
    }

    public float GetMass() { return mass; }
}
