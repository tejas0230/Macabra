using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public  AudioSource doorSource;
    public AudioClip doorOpen;
    public AudioClip doopClose;

    public bool isLocked;
    public bool isDoorOpen;
    [SerializeField] private float lerpSpeed;

    private void Awake()
    {
        doorSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(transform.rotation.y>-1f && isDoorOpen)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            doorSource.clip = doopClose;
            doorSource.Play();
        }
    }

    void CloseDoorAutomatic()
    {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * lerpSpeed);
    }
}
