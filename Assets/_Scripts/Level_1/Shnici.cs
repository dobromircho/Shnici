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
    Dictionary<int,GameObject> gramsText = new Dictionary<int, GameObject>();
    public float forceDown;
    public float forceUp;
    float timerRed;
    bool isJump;
    bool isZeroVelocity;
    bool isRed;

    void Start()
    {
        instance = this;
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
        if (timerRed >=0.5f && isRed)
        {
            color.material.color = Color.white;
            isRed = false;
        }
        if (rb.velocity.y == 0)
        {
            isJump = false;
        }
        if (!isJump)
        {
            if (!isZeroVelocity)
            {
                rb.velocity = Vector3.zero;
            }
            rb.AddForce(Vector3.down * forceDown, ForceMode.Force);
        }
        if (transform.position.y >= 10)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
        }
        if (transform.position.y <= -10)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, -10f, transform.position.z);
        }
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
            //GameManager.instance.IncreaseTrashLevel();
        }
        if (other.gameObject.tag == "badfish")
        {
            Instantiate(hitRedfish, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            //GameManager.instance.DecreaseEnergy(10);
            color.material.color = Color.red;
            isRed = true;
            timerRed = 0;
        }
    }
}

