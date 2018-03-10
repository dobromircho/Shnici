using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public static GameManager instance;
    public GameObject fish;
    public GameObject badFish;
    public Slider energy;
    public Button jump;
    public Button start;
    public Text gameOver;
    int fishCounter = 0;
    public float maxHeght;
    public float minHeght;
    AudioSource audioSource;
    float timer;
    float timerEnergy;


    void Start()
    {
        instance = this;
        energy.value = 100;
        Time.timeScale = 0;
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        timerEnergy += Time.deltaTime;

        if (timerEnergy >= 1)
        {
            DecreaseEnergy(1);
            timerEnergy = 0;
        }

        timer += Time.deltaTime;

        if (energy.value <= 1)
        {
            jump.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
        }
        if (timer >= Random.Range(1,3))
        {
            Instantiate(fish, new Vector3(22, Random.Range(minHeght, maxHeght), 5.54f), Quaternion.identity);
            fishCounter++;
            if (fishCounter >= Random.Range(3,5))
            {
                fishCounter = 0;
                Instantiate(badFish, new Vector3(22, Random.Range(minHeght + 1, maxHeght - 1), 5.54f), Quaternion.LookRotation(Vector3.forward));
            }
            timer = 0;
        }
    }
    public void DecreaseEnergy(int value)
    {
        if (value == 10)
        {
            audioSource.clip = sounds[1];
            audioSource.Play();
        }
        energy.value -= value;
    }

    public void IncreaseEnergy(int value)
    {
        audioSource.clip = sounds[0];
        audioSource.Play();
        energy.value += value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("Shnici_Swim", LoadSceneMode.Single);
        start.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
