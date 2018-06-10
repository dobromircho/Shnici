using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeIN : MonoBehaviour
{
    Image fade;
    public float timer;
    public static bool isFadeIN;

    void Start()
    {
        fade = GetComponent<Image>();
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        fade.color = new Color(0, 0, 0, timer);
        if (timer >= 1)
        {
            timer = 0;
            isFadeIN = true;
            gameObject.SetActive(false);
        }
    }
}
