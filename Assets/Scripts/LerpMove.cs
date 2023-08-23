using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMove : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float duration;
    float currentTime;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        transform.position = Vector3.Lerp(pointA.position, pointB.position,currentTime /duration);
    }
}
