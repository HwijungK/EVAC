using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LauncherHeadMover : MonoBehaviour
{
    public Transform head;
    AimPredictor ap;

    Vector2 dir;
    float angle;

    private void Awake()
    {
        ap = GetComponent<AimPredictor>();
    }
    private void Update()
    {
        dir = ap.GetStartingVelocity().normalized;
        angle = -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        head.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
