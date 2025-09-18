using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPredictor : MonoBehaviour
{
    public float predictTime;
    [RangeAttribute(0.001f, 1)] public float predictTimeInterval;
    public float renderLineTimeInterval;

    public ResearchNodeSO thrustersIIResearch;
    public float thrustersIMaxVel = 1.5f;
    public float thrustersIIMaxVel = 2.5f;
    [Range(0, 200)]
    public float maxAimAngle;

    public float startingVelocityMultiplier;
    ForceObject forceObject;

    public LineRenderer lr;
    private void Start()
    {
        lr.positionCount = (int) Mathf.Floor(predictTime / renderLineTimeInterval) - 1;
    }
    
    public void Update()
    {
        Aim();
    }

    public Vector2 GetStartingVelocity()
    {
        Vector2 dir = GetFixedDir();
        Vector2 rawVel = (dir) * startingVelocityMultiplier;
        //print("vel: " + rawVel.magnitude);
        if (!thrustersIIResearch.activated && rawVel.magnitude > thrustersIMaxVel)
        {
            rawVel = rawVel.normalized * thrustersIMaxVel;
        }
        else if (thrustersIIResearch.activated && rawVel.magnitude > thrustersIIMaxVel)
        {
            rawVel = rawVel.normalized * thrustersIIMaxVel;

        }
        return rawVel;
    }
    private Vector2 GetFixedDir()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float mag = dir.magnitude;
        if (Vector2.Angle(dir,transform.up) > maxAimAngle/2)
        {
            float adjustedAngle = transform.rotation.eulerAngles.z;
            adjustedAngle = Mathf.Atan2(transform.up.x, transform.up.y) * 180 / Mathf.PI;
            //print(adjustedAngle);
            if (Vector2.SignedAngle(dir,transform.up) > 0)
            {
                //print("Angle is " + adjustedAngle + " and it is tilted by " + (maxAimAngle / 2f));
                adjustedAngle += maxAimAngle / 2f;
            }
            else
            {
                //print("Angle is " + adjustedAngle + " and it is tilted by " + (maxAimAngle / 2f));
                adjustedAngle -= maxAimAngle / 2f;
            }
            
            dir = new Vector2(Mathf.Sin(Mathf.Deg2Rad * adjustedAngle), Mathf.Cos(Mathf.Deg2Rad * adjustedAngle)) * mag;
        }
        return dir;
    }
    private void Aim()
    {
        Vector2 position = transform.position;
        Vector2 velocity = GetStartingVelocity();

        float lastRenderTime = 0;
        int i = 0;

        float time = 0;
        while (i < lr.positionCount)
        {
            if (time >= renderLineTimeInterval + lastRenderTime)
            {
                lastRenderTime = time;
                lr.SetPosition(i, position);
                i++;
            }
            Vector2 force = FindObjectOfType<PhysicsManager>().GetForce(forceObject, position);
            Vector2 acc = force / forceObject.GetComponent<Rigidbody2D>().mass;
            velocity += acc * predictTimeInterval;
            Vector2 newPosition = position + velocity * predictTimeInterval;
            time += predictTimeInterval;
            position = newPosition;
            
            
        }

        int lasti = i;
        while (i < lr.positionCount)
        {
            lr.SetPosition(i, lr.GetPosition(lasti));
            i++;
        }

    }

    public void SetForceObject(ForceObject fo)
    {
        forceObject = fo;
    }

    public void OnEnable()
    {
        lr.enabled= true;
    }
    public void OnDisable()
    {
        lr.enabled = false;
    }
}
