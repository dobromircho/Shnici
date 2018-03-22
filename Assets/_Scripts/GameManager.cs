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
    public Text collectedGramsText;
    public GameObject gramsPanel;
    public float maxHeght;
    public float minHeght;

    AudioSource audioSource;
    float timer = -3;
    float timerEnergy;
    int fishCounter = 0;
    float collectedGrams;
    float maxCollectedGramsLVL1 = 2000;
    bool isGameStarted;
    int randomTrash;
    int trashNumber;

    void Start()
    {
        gramsPanel.SetActive(false);
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {

        
        timerEnergy += Time.deltaTime;
        timer += Time.deltaTime;
        
        if (FadeOut.isFadeOut)
        {
            Time.timeScale = 0;
            FadeOut.isFadeOut = false;
        }
        
        if (timerEnergy >= 1)
        {
           // DecreaseEnergy(1);
            timerEnergy = 0;
        }

        if (trashLevel.value >= 2000)
        {
            jump.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
        }

        if (timer >= Random.Range(1.5f,3))
        {
            CreateTrash();
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

    public void IncreaseTrashLevel()
    {
        
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        gramsPanel.SetActive(true);
        start.gameObject.SetActive(false);
        storyStart.SetActive(false);
        shniciSaying.SetActive(true);
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
        Instantiate(trash[randomTrash], new Vector3(22, Random.Range(minHeght, maxHeght), 5.54f), Quaternion.identity);
        trashNumber = randomTrash;
        fishCounter++;
        if (fishCounter >= Random.Range(3, 6))
        {
            fishCounter = 0;
            Instantiate(badFish, new Vector3(22, Random.Range(minHeght + 1, maxHeght - 1), 5.54f), Quaternion.LookRotation(Vector3.forward));
        }
        timer = 0;
    }

    public void VolumeChange()
    {
        AudioListener.volume = soundLevel.value;
    }
}
