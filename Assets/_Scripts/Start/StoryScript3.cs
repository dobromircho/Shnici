using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoryScript3 : MonoBehaviour
{
    public float timeToDesapeare;
    Image bground;
    Text text;
    Image[] children = new Image[20];
    float timerIN = -1;
    float timerOUT = 1;
    Color bg;
    bool isFading;

    void Start()
    {

        gameObject.SetActive(false);
        bground = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        children = GetComponentsInChildren<Image>();
        bg = bground.color;
        foreach (var item in children)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, 0);
        }
        bground.color = new Color(1, 1, 1, 0);
        text.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (isFading)
        {
            timerOUT -= Time.deltaTime;
            bground.color = new Color(bg.r, bg.g, bg.b, timerOUT);
            text.color = new Color(1, 1, 1, timerOUT);
            if (timerOUT <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            timerIN += Time.deltaTime;
            bground.color = new Color(bg.r, bg.g, bg.b, timerIN);
            text.color = new Color(1, 1, 1, timerIN);
            foreach (var item in children)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, timerIN);
            }
            if (timerIN >= timeToDesapeare)
            {
                isFading = true;
            }
        }
    }
}
