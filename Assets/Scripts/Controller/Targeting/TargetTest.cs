using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTest : MonoBehaviour
{
    public float speed = 3f;

    float k = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 5) k = -1;
        else if (transform.position.x < -5) k = 1;

        transform.position += k * speed * Vector3.right * Time.deltaTime;
    }
}
