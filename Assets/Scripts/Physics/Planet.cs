using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float rotationalSpeed;
    float radius;
    public int buildPortCount;
    public float buildPortExtraDst = 0.1f;
    public Transform BuildPortPrefab;
    public bool buildable;


    private void Awake()
    {
        radius = transform.localScale.x;

        // Initialize BuildPorts

        float buildPortAngle = 360f / buildPortCount;
        for (int i = 0;  i< buildPortCount; i++)
        {
            Transform newBuildPort = Instantiate(BuildPortPrefab, transform, true);
            newBuildPort.localPosition = transform.up * .5f;
            newBuildPort.Translate(Vector2.up * buildPortExtraDst, Space.World);
            newBuildPort.RotateAround(transform.position, Vector3.forward, i * buildPortAngle);
            if (buildable)
            {
                newBuildPort.gameObject.SetActive(true);
            }
            else
            {
                newBuildPort.gameObject.SetActive(false);
            }
        }
    }
    public void Colonize()
    {
        buildable = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public virtual void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 rotation = (transform.rotation).eulerAngles;
        rotation.z += rotationalSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
    }

}