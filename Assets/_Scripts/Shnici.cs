using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shnici : MonoBehaviour
{
    AudioSource sound;
    Rigidbody rb;
    SkinnedMeshRenderer color;
    public GameObject hitRedfish;
    public GameObject hitfish;
    public GameObject particle;
    public float forceDown;
    public float forceUp;
    float timerRed;
    bool isJump;
    bool isZeroVelocity;
    bool isRed;

    void Start()
    {
        color = GameObject.FindGameObjectWithTag("color").GetComponent<SkinnedMeshRenderer>();
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
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
        if (other.gameObject.tag == "fish")
        {
            Instantiate(hitfish, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            GameManager.instance.IncreaseEnergy(1);
        }
        if (other.gameObject.tag == "badfish")
        {
            Instantiate(hitRedfish, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            GameManager.instance.DecreaseEnergy(10);
            color.material.color = Color.red;
            isRed = true;
            timerRed = 0;
        }
    }
}

