using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public Material bgMaterial;
    public float speed;
    void Start()
    {
        bgMaterial.mainTextureOffset = new Vector2(0, 0);
    }
    
    void FixedUpdate()
    {
        bgMaterial.mainTextureOffset += new Vector2(speed, 0);
        if (bgMaterial.mainTextureOffset.x >= 5)
        {
            bgMaterial.mainTextureOffset = new Vector2(0, 0);
        }
    }
}
