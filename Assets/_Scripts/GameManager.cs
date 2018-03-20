using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public static GameManager instance;
    public GameObject[] trash;
    public GameObject badFish;
    public Slider trashLevel;
    public Slider soundLevel;
    public Button jump;
    public Button start;
    public GameObject menuPanel;
    public GameObject storyStart;
    public GameObject shniciSaying;
    public Text gameOver;
    public float maxHeght;
    public float minHeght;
    AudioSource audioSource;
    float timer = -3;
    float timerEnergy;
    int fishCounter = 0;
    bool isGameStarted;

    void Start()
    {
        instance = this;
        trashLevel.value = 32;
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (FadeOut.isFadeOut)
        {
            Time.timeScale = 0;
            FadeOut.isFadeOut = false;
        }

        AudioListener.volume = soundLevel.value;

        timerEnergy += Time.deltaTime;

        if (timerEnergy >= 1)
        {
           // DecreaseEnergy(1);
            timerEnergy = 0;
        }

        timer += Time.deltaTime;

        if (trashLevel.value >= 100)
        {
            jump.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
        }
        if (timer >= Random.Range(1.5f,3))
        {
            Instantiate(trash[Random.Range(0, trash.Length)], new Vector3(22, Random.Range(minHeght, maxHeght), 5.54f), Quaternion.identity);
            fishCounter++;
            if (fishCounter >= Random.Range(3,5))
            {
                fishCounter = 0;
                Instantiate(badFish, new Vector3(22, Random.Range(minHeght + 1, maxHeght - 1), 5.54f), Quaternion.LookRotation(Vector3.forward));
            }
            timer = 0;
        }
    }
    /*public void DecreaseEnergy(int value)
    {
        if (value == 10)
        {
            audioSource.clip = sounds[1];
            audioSource.Play();
        }
        trashLevel.value -= value;
    }*/

    public void IncreaseTrashLevel(int value)
    {
        audioSource.clip = sounds[0];
        audioSource.Play();
        trashLevel.value += value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        start.gameObject.SetActive(false);
        storyStart.SetActive(false);
        shniciSaying.SetActive(true);
        isGameStarted = true;
        Time.timeScale = 1;
    }

    public void MenuPanelShow()
    {
        audioSource.clip = sounds[2];
        audioSource.Play();
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void MenuPanelHide()
    {
        audioSource.clip = sounds[2];
        audioSource.Play();
        menuPanel.SetActive(false);
        if (isGameStarted)
        {
            Time.timeScale = 1;
        }
    }
}
