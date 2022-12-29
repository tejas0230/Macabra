using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private AudioSource doorSource;
    [SerializeField]
    private DoorProperties doorProperties;

    [Header("Audio Variables")]
    [SerializeField]
    private AudioClip doorOpen;
    [SerializeField]
    private AudioClip doorClose;
    [SerializeField]
    private AudioClip doorLocked;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (doorProperties.isInteracting && doorProperties.isLocked)
        {
            if (!doorSource.isPlaying)
                doorSource.PlayOneShot(doorLocked);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            if (counter == 0 || counter == 1)
                counter++;
            if (counter == 1)
                return;
            if (!doorSource.isPlaying)
                doorSource.PlayOneShot(doorClose);
            print("played");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            if (!doorSource.isPlaying)
                doorSource.PlayOneShot(doorOpen);
        }
    }
}

