using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    public int health;
    protected bool spawnCollisionBuffer;
    protected Transform spawnCollisionBufferedPlanet;
    public UnityEvent<int> HealthChanged;
    public UnityEvent Destroyed;

    public LayerMask spawnCollisionBufferLayer;

    public SoundSO damageSound;
    public ParticleSystem[] explodeParticle;

    public ParticleSystem healthIndicator;
    public float[] healthIndicatorEmissions;
    public ParticleSystem criticalHealthIndicator;
    


    private void Awake()
    {
        health = startingHealth;

        spawnCollisionBuffer = false;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, transform.lossyScale.x / 2f, Vector2.zero, 0, spawnCollisionBufferLayer);
        //print("Searching For Spawn Collider");
        if (hit.transform != null) {
            print("Buffer Activated");
            spawnCollisionBufferedPlanet = hit.transform;
            spawnCollisionBuffer = true;
        }

        // set particles
        if (healthIndicator != null)
        {
            healthIndicator = Instantiate(healthIndicator, transform.position, Quaternion.identity, transform);
            var emission = healthIndicator.emission;
            emission.rateOverTime = healthIndicatorEmissions[0];
            criticalHealthIndicator = Instantiate(criticalHealthIndicator, transform.position, Quaternion.identity, transform);
            criticalHealthIndicator.Pause();

            var critHealthShape = criticalHealthIndicator.shape;
            var healthShape = healthIndicator.shape;
            critHealthShape.radius = transform.lossyScale.x /2;
            healthShape.radius = transform.lossyScale.x /2;
        }
    }
    private void FixedUpdate()
    {
        if (spawnCollisionBuffer)
        {
            print("Buffer is active during fixed Update");
            if (GetComponent<Collider2D>().IsTouchingLayers(spawnCollisionBufferLayer))
            {
                print("Strill Touchiling l=kanment')");
            }
            else
            {
                spawnCollisionBuffer = false;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        HealthChanged?.Invoke(health);
        if (health <= 0)
        {
            Die();
        }
        
        if (damageAmount != 0)
        {
            if (gameObject.tag == "MainPlanet")
            {
                SoundManager.instance.IncreaseMusicIndex();
            }
            SoundManager.instance.PlaySound(damageSound);
        }

        if (healthIndicator != null)
        {
            if (health == 1)
            {
                criticalHealthIndicator.Play();
            }
            int particleIndex = (int)Mathf.Floor(((float)(startingHealth - health) / startingHealth) * healthIndicatorEmissions.Length);
            var emission = healthIndicator.emission;
            if (particleIndex < healthIndicatorEmissions.Length)
            {
                print(healthIndicatorEmissions[particleIndex] * transform.lossyScale.x * transform.lossyScale.x);
                emission.rateOverTime = healthIndicatorEmissions[particleIndex] * transform.lossyScale.x;
            }
            

        }


    }
        

    public void Die()
    {
        Destroyed?.Invoke();
        foreach (ParticleSystem p in explodeParticle)
        {
            Destroy(Instantiate(p, transform.position, Quaternion.identity).gameObject, p.main.duration);
        }
        Destroy(gameObject);
    }

    public virtual void  OnCollisionEnter2D(Collision2D collision)
    {
        
        if (spawnCollisionBuffer && collision.transform == spawnCollisionBufferedPlanet)
        {
            //print("Succesfully Buffered");
        }
        else
        {
            if (collision.transform.TryGetComponent(out Damageable other))
            {
                //print("Collision Called from " + transform);
                //print("taking " + Mathf.Min(health, other.health) + "Damage");
                int damageToTake = Mathf.Min(health, other.health);
                TakeDamage(damageToTake);
                other.TakeDamage(damageToTake);
            }
        } 

    }
    public void SpawnParticle(ParticleSystem p)
    {
        Destroy(Instantiate(p, transform.position, Quaternion.identity).gameObject, p.main.duration * 2);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, transform.lossyScale.x / 2f);
    }
}
