using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramsText : MonoBehaviour
{

    Transform target;
    float timer;
    Rigidbody rb;
    float lerp = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("target").transform;
    }
    
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 1.3f)
        {
            Destroy(gameObject);
        }
        lerp += timer;
        lerp /=20;
        rb.MovePosition(new Vector3 (Mathf.Lerp(transform.position.x, target.position.x, lerp)
                                   , Mathf.Lerp(transform.position.y, target.position.y, lerp), 0));
    }
}
