using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interceptor : MonoBehaviour
{
    public float range = 1;
    public float force = 5;

    Transform target;
    bool targetting;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!targetting)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (Collider2D hit in hits)
            {
                if (hit.transform.tag == "Astroid")
                {
                    target = hit.transform;
                    targetting = true;
                }
            }
        }
        else
        {
            if (target != null && target.transform != null)
            {
                rb.AddForce((target.position - transform.position).normalized * force, ForceMode2D.Force);
                rb.velocity = Vector2.Lerp(rb.velocity, (target.position - transform.position).normalized * rb.velocity.magnitude, 0.8f);

            }
            else
            {
                targetting = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (targetting)
        {
            Gizmos.DrawLine(transform.position, target? target.position:Vector3.zero);
        }
    }
}
