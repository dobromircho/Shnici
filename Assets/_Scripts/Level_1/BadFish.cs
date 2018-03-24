using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadFish : MonoBehaviour
{
    MeshRenderer color;
    float speed;
    float timer;

    void Start()
    {
        speed = Random.Range(10, 15);
        color = GetComponentInChildren<MeshRenderer>();
        //transform.Rotate(0, 0, 60);
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0)
        {
            color.material.color = Color.red;
        }
        if (timer >= 0.2f)
        {
            color.material.color = Color.white;
            timer = -0.2f;
        }
        this.transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}
