using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeOut : MonoBehaviour
{
    Image fade;
    public float timer;
    public float timeToFadeOut;
    public static bool isFadeOut;

    void Start()
    {
        fade = GetComponent<Image>();
        timer = 1;
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        fade.color = new Color(0, 0, 0, timer);
        if (timer <= 0)
        {
            timer = 1;
            isFadeOut = true;
            gameObject.SetActive(false);
        }
    }
}
