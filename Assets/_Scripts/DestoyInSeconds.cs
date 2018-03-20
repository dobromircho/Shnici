using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyInSeconds : MonoBehaviour
{
    public float seconds;
    void Start()
    {
        Destroy(gameObject, seconds);
    }
}
