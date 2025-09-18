using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : Planet
{
    public float orbitSpeed;
    public Planet motherPlanet;
    Vector3 motherPlanetLastPos;

    public override void Update()
    {
        base.Update();
        if (motherPlanet != null)
        {
            motherPlanetLastPos = motherPlanet.transform.position;
        }
        
        Orbit();

    }
    private void Orbit()
    {
        if (motherPlanet != null)
        {
            transform.RotateAround(motherPlanet.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(motherPlanetLastPos, Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }
}
