using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public GameObject story4;
    public GameObject fadeIN;
    public float timer;
    public float timeTo1;
    public float timeTo2;
    public float timeTo3;

    float timeToStart = 1;
    float timerStarter;
    bool isStarted;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeTo1)
        {
            story1.SetActive(true);
        }
        if (timer >= timeTo2)
        {
            story4.SetActive(true);
            story1.SetActive(false);
            story2.SetActive(true);
        }
        if (timer >= timeTo3)
        {
            story2.SetActive(false);
            story3.SetActive(true);
        }
        if (isStarted)
        {
            fadeIN.SetActive(true);
            timerStarter += Time.deltaTime;
            if (timer >= timeToStart)
            {
                SceneManager.LoadScene("Shnici_Level_1", LoadSceneMode.Single);
            }
        }
        
    }

    public void StartGame()
    {
        isStarted = true;
    }
}
