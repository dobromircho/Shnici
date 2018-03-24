using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shnici : MonoBehaviour
{
    public static Shnici instance;
    Dictionary<string, int> trashGrams = new Dictionary<string, int>();
    AudioSource sound;
    Rigidbody rb;
    SkinnedMeshRenderer color;
    public GameObject hitRedfish;
    public GameObject hitfish;
    public GameObject particle;
    public GameObject[] gramsPrefabs;
    public Slider oxygenLevel;
    Dictionary<int,GameObject> gramsText = new Dictionary<int, GameObject>();
    Image oxigenColor;
    Color oxigenRealColor;
    public float forceDown;
    public float forceUp;
    public float oxyRecharge;
    public float oxyDecharge;
    float timerRed;
    bool isJump;
    bool isZeroVelocity;
    bool isRed;

    void Start()
    {
        instance = this;
        oxigenColor = GameObject.FindGameObjectWithTag("oxygenColor").GetComponent<Image>();
        oxigenRealColor = oxigenColor.color;
        color = GameObject.FindGameObjectWithTag("color").GetComponent<SkinnedMeshRenderer>();
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();

        trashGrams.Add("bag", 5);
        trashGrams.Add("can", 10);
        trashGrams.Add("yogurt", 20);
        trashGrams.Add("net", 80);
        trashGrams.Add("flipflop", 120);

        gramsText.Add(5, gramsPrefabs[0]);
        gramsText.Add(10, gramsPrefabs[1]);
        gramsText.Add(20, gramsPrefabs[2]);
        gramsText.Add(80, gramsPrefabs[3]);
        gramsText.Add(120, gramsPrefabs[4]);
    }
    
    void FixedUpdate()
    {
        timerRed += Time.deltaTime;
        ColorChange();
        CheckVelocity();
        PushBodyDown();
        HoldBodyUp(10);
        HoldBodyDown(-10);
    }

    public void Jump()
    {
        isJump = true;
        rb.velocity = Vector3.zero;
        isZeroVelocity = true;
        rb.AddForce(Vector3.up * forceUp, ForceMode.Impulse);
        sound.Play();

    }

    void OnTriggerEnter (Collider other)
    {
        if (trashGrams.ContainsKey(other.gameObject.tag))
        {
            GameObject gramText = gramsText[trashGrams[other.gameObject.tag]];
            Instantiate(hitfish, other.transform.position, Quaternion.identity);
            Instantiate(gramText, other.transform.position, Quaternion.identity);
            GameManager.instance.IncreaseCollectedGrams(trashGrams[other.gameObject.tag]);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "badfish")
        {
            Instantiate(hitRedfish, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            GameManager.instance.DecreaseOxygen(10);
            color.material.color = Color.red;
            oxigenColor.color = Color.red;
            isRed = true;
            timerRed = 0;
        }
    }

    void HoldBodyUp(int value)
    {
        if (transform.position.y >= value)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
            GameManager.instance.IncreaseOxygen(2);
            oxigenColor.color = Color.green;
            isRed = true;
            timerRed = 0;

        }
    }
    void HoldBodyDown(int value)
    {
        if (transform.position.y <= value)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
            GameManager.instance.DecreaseOxygen(2);
            color.material.color = Color.red;
            oxigenColor.color = Color.red;
            isRed = true;
            timerRed = 0;
        }
    }
    void PushBodyDown()
    {
        if (!isJump)
        {
            if (!isZeroVelocity)
            {
                rb.velocity = Vector3.zero;
            }
            rb.AddForce(Vector3.down * forceDown, ForceMode.Force);
        }
    }
    void ColorChange()
    {
        if (timerRed >= 0.5f && isRed)
        {
            color.material.color = Color.white;
            oxigenColor.color = oxigenRealColor;
            isRed = false;
        }
    }
    void CheckVelocity()
    {
        if (rb.velocity.y == 0)
        {
            isJump = false;
        }
    }
}

