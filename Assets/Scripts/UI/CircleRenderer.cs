using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    LineRenderer lr;

    [SerializeField] private float radius;
    [SerializeField] private Gradient color;
    [SerializeField] private int verticesCount;
    [SerializeField] private float width;
    Transform[] verticesTransforms;

    GameObject pointHolder;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.loop = true;
        lr.colorGradient = color;
        lr.positionCount = verticesCount;
        lr.startWidth = width;
        lr.endWidth = width;

        pointHolder = new GameObject("Point Holder");
        pointHolder.transform.parent = transform;
        pointHolder.transform.localPosition = Vector3.zero;

        verticesTransforms= new Transform[verticesCount];
        for (int i = 0; i < verticesCount; i++)
        {
            verticesTransforms[i] = new GameObject("point " + i).transform;
            verticesTransforms[i].SetParent(pointHolder.transform);
            verticesTransforms[i].localPosition = Vector2.up * radius;
        }

        UpdateCircle();
    }

    private void Update()
    {
        //UpdateCircle();
    }

    void UpdateCircle()
    {


        for (int i  = 0; i <verticesCount; i++)
        {
            verticesTransforms[i].RotateAround(transform.position, Vector3.forward, i * 360f / verticesCount);
            lr.SetPosition(i, verticesTransforms[i].localPosition);
        }
        
    }
}
