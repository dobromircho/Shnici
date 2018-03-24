using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    float fishSpeed;
    float rotation;
    int[] rotDirection = { -1, 1 };
    int direction;

    private void Start()
    {
        fishSpeed = Random.Range(4, 10);
        rotation = Random.Range(20, 80);
        direction = rotDirection[Random.Range(0, 2)];
    }
    private void Update()
    {
        this.transform.Translate(-fishSpeed * Time.deltaTime, 0, 0, Space.World);
        transform.Rotate(0, 0, rotation * Time.deltaTime * direction);
    }
}

