using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PhoneScript : MonoBehaviour,IInteractable
{
    public AudioSource phoneSource;
    public AudioClip ring;
    public AudioClip iCanSeeYou;
    public float MaxRange => 2;
    public GameObject crossHair;
    public GameObject hand;
    public void OnEndHover()
    {
        hand.SetActive(false);
        crossHair.SetActive(true);
    }

    public void OnInteract()
    {
        phoneSource.loop = false;
        phoneSource.Stop();
        phoneSource.PlayOneShot(iCanSeeYou);
    }

    public void OnStartHover()
    {
        crossHair.SetActive(false);
        hand.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
