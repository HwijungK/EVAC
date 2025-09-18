using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colonizer : Damageable
{
    public new void OnCollisionEnter2D(Collision2D collision)
    {
        if (spawnCollisionBuffer && collision.transform == spawnCollisionBufferedPlanet)
        {
            print("Succesfully Buffered");
        }
        else
        {
            if (collision.transform.TryGetComponent(out Damageable other))
            {
                if (collision.transform.TryGetComponent<Planet>(out Planet p))
                {
                    if (!p.buildable)
                    {
                        p.Colonize();
                    }
                }
                else
                {
                    //print("Collision Called from " + transform);
                    //print("taking " + Mathf.Min(health, other.health) + "Damage");
                    int damageToTake = Mathf.Min(health, other.health);
                    TakeDamage(damageToTake);
                    other.TakeDamage(damageToTake);
                }
                
            }
        }
    }
}
