using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject instrPanelPage1;
    public GameObject instrPanelPage2;
    public GameObject story4;
    public GameObject fadeIN;
    public GameObject fadeOUT;

    public float timer = -1;
    public float timeTo1;
    public float timeTo2;
    public float timeTo3;

    float timeToStart = 1;
    float timerStarter;
    bool isStarted;


    void Update()
    {
        timer += Time.deltaTime;
        
        if (isStarted)
        {
            fadeIN.SetActive(true);
            timerStarter += Time.deltaTime;
            if (timer >= timeToStart)
            {
                SceneManager.LoadScene("Shnici_Level_1", LoadSceneMode.Single);
            }
        }
        if (FadeIN.isFadeIN)
        {
            fadeOUT.SetActive(true);
            FadeIN.isFadeIN = false;
        }
        if (FadeOut.isFadeOut)
        {
            FadeOut.isFadeOut = false;
        }
    }

    public void StartGame()
    {
        isStarted = true;
    }

    public void OpenInstructionPanel()
    {
        instructionPanel.SetActive(true);
        FadeINOUT();
    }
    public void CloseInstructionPanel()
    {
        instructionPanel.SetActive(false);
        FadeINOUT();
    }

    public void FadeINOUT()
    {
        fadeIN.SetActive(true);
    }
}
