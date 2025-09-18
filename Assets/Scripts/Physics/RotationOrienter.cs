using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOrienter : MonoBehaviour
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(Vector3.forward * -Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg);
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * -Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg);
    }
}
