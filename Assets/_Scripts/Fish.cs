using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float fishSpeed;
    private void Update()
    {
        this.transform.Translate(-fishSpeed * Time.deltaTime, 0, 0);
    }
}

