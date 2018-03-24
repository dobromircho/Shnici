using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public Material bgMaterial;
    
    void Start()
    {
        bgMaterial.mainTextureOffset = new Vector2(0, 0);
    }
    
    void FixedUpdate()
    {
        bgMaterial.mainTextureOffset += new Vector2(0.002f, 0);
        if (bgMaterial.mainTextureOffset.x >= 5)
        {
            bgMaterial.mainTextureOffset = new Vector2(0, 0);
        }
    }
}
