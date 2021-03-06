﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public static GameManager instance;
    public GameObject[] trash;
    public GameObject[] badFish;
    public GameObject gramsPanel;
    public GameObject jump;
    public GameObject menuPanel;
    public GameObject shnici;
    public GameObject fadeIn;

    public Slider oxigenLevel;
    public Slider trashLevel;
    public Slider soundLevel;
    public Text gameOver;
    public Text collectedGramsText;
    public Text oxygenText;
    public float maxHeght;
    public float minHeght;

    AudioSource audioSource;
    Animator shniciAnim;
    float timerWin;
    float timer = -3;
    float timerEnergy;
    float timerGameOver;
    float collectedGrams;
    float maxCollectedGramsLVL1 = 200;
    bool isGameStarted;
    bool isGameOver;
    int randomTrash;
    int trashNumber;
    int fishCounter = 0;


    void Start()
    {
        shniciAnim = shnici.GetComponent<Animator>();
        gramsPanel.SetActive(false);
        oxigenLevel.value = 100;
        instance = this;
        audioSource = GetComponent<AudioSource>();
        StartGame();
    }
    
    void Update()
    {
        timerEnergy += Time.deltaTime;
        timer += Time.deltaTime;
        oxygenText.text = oxigenLevel.value + " %";
        
        if (FadeOut.isFadeOut)
        {
            FadeOut.isFadeOut = false;
        }
        if (isGameOver)
        {
            timerGameOver += Time.deltaTime;
            if (timerGameOver >= 5)
            {
                fadeIn.SetActive(true);
            }
            if (timerGameOver >= 7)
            {
                SceneManager.LoadScene("Start_Scene");
            }
        }
        if (timerEnergy >= 1)
        {
            DecreaseOxygen(1);
            timerEnergy = 0;
        }

        if (trashLevel.value >= maxCollectedGramsLVL1)
        {
            timerWin += Time.deltaTime;
            shniciAnim.SetTrigger("winstart");
            jump.gameObject.SetActive(false);
            Shnici.instance.rb.velocity = Vector3.zero;
            Shnici.instance.cc.enabled = false;
            
            Shnici.instance.transform.position = new Vector3(
                Shnici.instance.transform.position.x,
                Mathf.Lerp(Shnici.instance.transform.position.y, 0,timerWin),
                Shnici.instance.transform.position.z);

            timerEnergy = 0;
            timer = 0;
            gameOver.gameObject.SetActive(true);
            isGameOver = true;
        }
        if (oxigenLevel.value <= 1)
        {
            jump.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
            isGameOver = true;
        }

        if (timer >= Random.Range(1.5f,3))
        {
            CreateTrash();
        }
    }
    public void DecreaseOxygen(int value)
    {
        if (value == 20)
        {
            audioSource.clip = sounds[1];
            audioSource.volume = 0.13f;
            audioSource.Play();
        }
        oxigenLevel.value -= value;
    }

    public void IncreaseOxygen(int value)
    {
        oxigenLevel.value += value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        gramsPanel.SetActive(true);
        isGameStarted = true;
        Time.timeScale = 1;
    }

    public void MenuPanelShow()
    {
        jump.gameObject.SetActive(false);
        audioSource.clip = sounds[2];
        audioSource.Play();
        menuPanel.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void MenuPanelHide()
    {
        jump.gameObject.SetActive(true);
        audioSource.clip = sounds[2];
        audioSource.Play();
        menuPanel.SetActive(false);
        if (isGameStarted)
        {
            Time.timeScale = 1;
        }
    }

    public void IncreaseCollectedGrams(int grams)
    {
        audioSource.clip = sounds[0];
        audioSource.volume = 0.5f;
        audioSource.Play();
        trashLevel.value += grams;
        collectedGrams += grams;
        collectedGramsText.text = collectedGrams + "/" + maxCollectedGramsLVL1 + "гр.";
    }

    private void CreateTrash()
    {
        randomTrash = Random.Range(0, trash.Length);
        if (randomTrash == trashNumber && randomTrash != trash.Length -1)
        {
            randomTrash++;
        }
        Instantiate(trash[randomTrash], new Vector3(22, Random.Range(minHeght+1, maxHeght), 5.54f), Quaternion.identity);
        trashNumber = randomTrash;
        fishCounter++;
        if (fishCounter >= Random.Range(1, 2))
        {
            fishCounter = 0;
            Instantiate(badFish[Random.Range(0,badFish.Length)], new Vector3(22, Random.Range(minHeght + 1, maxHeght), 5.54f), Quaternion.LookRotation(Vector3.forward));
        }
        timer = 0;
    }

    public void VolumeChange()
    {
        AudioListener.volume = soundLevel.value;
    }
}
