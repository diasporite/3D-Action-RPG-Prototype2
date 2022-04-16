using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float theta = 0;
    public float dist = 5;

    void Update()
    {
        theta += 90 * Time.deltaTime;
        theta = theta % 360;

        transform.position = new Vector3(-dist * Mathf.Cos(theta * Mathf.Deg2Rad), 0, dist * Mathf.Sin(theta * Mathf.Deg2Rad));
    }
}
