using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleFish : MonoBehaviour
{
    public float speed = 1;
    public float timeToMove;
    float timer;
    
    void Start()
    {

    }
    
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(speed * Time.deltaTime, speed * Time.deltaTime, 0);
        
        if (timer >= timeToMove)
        {
            speed *= -1;
            timer = 0;
        }
    }
}
