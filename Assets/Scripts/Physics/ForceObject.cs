using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObject : MonoBehaviour
{
    // Debuger
    //public float predictTime;
    //[RangeAttribute(0.001f, 1)] public float predictTimeInterval;
    public Vector2 startingVelocity;
    public Rigidbody2D rb;
    public int power = 1;
    // Start is called before the first frame update
    void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity = startingVelocity;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 force = PhysicsManager.instance.GetForce(this);
        //print("experience force from planets: " + force);
        rb.AddForce(force);
    }
    public float GetMass() { return rb.mass; }

    /*private void OnDrawGizmos()
    {
        Vector2 position = transform.position;
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

        float time = 0;
        while (time < predictTime)
        {
            Vector2 force = FindObjectOfType<PhysicsManager>().GetForce(this, position);
            Vector2 acc = force / GetComponent<Rigidbody2D>().mass;
            velocity += acc * predictTimeInterval;
            Vector2 newPosition = position + velocity * predictTimeInterval;
            time += predictTimeInterval;

            Gizmos.DrawLine(position, newPosition);
            position = newPosition;
        }
    }*/
}
