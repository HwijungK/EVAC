using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager instance;

    public float G = 1;
    public List<MassBody> massBodies;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
    }

    public Vector2 GetForce(ForceObject forceBody)
    {
        return GetForce(forceBody, forceBody.transform.position);
       
    }
    public Vector2 GetForce(ForceObject forceBody, Vector2 position)
    {
        Vector2 sumForce = Vector2.zero;
        foreach (MassBody massBody in massBodies)
        {
            if (massBody == null) continue;
            float mag = G * forceBody.GetMass() * massBody.GetMass() / Mathf.Pow(Vector2.Distance(position, massBody.transform.position), 2);
            Vector2 dir = massBody.transform.position - (Vector3) position;
            Vector2 force = mag * dir;
            sumForce += force;
        }
        return sumForce;
    }
}
