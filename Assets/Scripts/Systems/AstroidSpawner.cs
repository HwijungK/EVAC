using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public Vector2 topRight, botLeft;

    
    public float minSpeed, maxSpeed;
    public float maxAngleVar;

    [Header("Difficulty")]
    public Vector2[] spawnRatesByTime;
    //public float startSpawnTimeInterval;
    //public float endSpawnTimeInterval;
    //public float timeToMaxDifficulty;
    float lastSpawnTime;
    float currSpawnRate;
    

    public ForceObject Astroid;
    private void Start()
    {
        currSpawnRate = spawnRatesByTime[0].y;
    }
    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAstroid();
        }*/

        
        if (lastSpawnTime + currSpawnRate <= Time.timeSinceLevelLoad)
        {
            for (int i = 0; i < spawnRatesByTime.Length; i++)
            {
                if (spawnRatesByTime[i].x > Time.timeSinceLevelLoad)
                {
                    break;
                }
                currSpawnRate = spawnRatesByTime[i].y;
            }
            //currSpawnRate = Mathf.Lerp(startSpawnTimeInterval, endSpawnTimeInterval, Time.time / timeToMaxDifficulty); // might die
            lastSpawnTime = Time.timeSinceLevelLoad;
            SpawnAstroid();
        }
    }

    public void SpawnAstroid()
    {
        //Find Spawn Position
        Vector2 spawnPosition = new Vector2(Random.Range(topRight.x, botLeft.x), Random.Range(topRight.y, botLeft.y));
        switch(Random.Range(0, 4))
        {
            case 0:
                spawnPosition.x = topRight.x;
                break;
            case 1:
                spawnPosition.x = botLeft.x;
                break;
            case 2:
                spawnPosition.y = botLeft.y;
                break;
            case 3:
                spawnPosition.y = topRight.y;
                break;
        }
            
        float speed = Random.Range(minSpeed, maxSpeed);
        float angle = Mathf.Atan2(-spawnPosition.x, -spawnPosition.y);
        float dirAngle = angle + Random.Range(-maxAngleVar * Mathf.PI / 180, maxAngleVar * Mathf.PI / 180);
        Vector2 dir = new Vector2(Mathf.Sin(dirAngle), Mathf.Cos(dirAngle));
        Vector2 velocity = dir * speed;
        Instantiate(Astroid, spawnPosition, Quaternion.identity).startingVelocity = velocity;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(topRight, 0.1f);
        Gizmos.DrawSphere(botLeft, 0.1f);
    }
}
