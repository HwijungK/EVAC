using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvacShip : MonoBehaviour
{
    public ParticleSystem particles;
    public ParticleSystem trail;

    public float jumpDelay;
    float spawnTime;

    public SoundSO evacSound;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(particles, transform.position, Quaternion.identity, transform);
        spawnTime = Time.time;
 
        SoundManager.instance.PlaySound(evacSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime + jumpDelay)
        {
            spawnTime += 100000;
            GetComponent<Rigidbody2D>().velocity *= 15;
            Instantiate(trail, transform.position, Quaternion.identity, transform);
            Destroy(gameObject, 2);
            Invoke("Escape", 1);
        }
    }
    private void Escape()
    {
        EvacCounter.instance.IncreaseEvacCount();
    }
    private void OnDestroy()
    {
        SoundManager.instance.IncreaseMusicIndex();
    }
}
