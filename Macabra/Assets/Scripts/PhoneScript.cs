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

    public void OnEndHover()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract()
    {
        phoneSource.loop = false;
        phoneSource.Stop();
        phoneSource.PlayOneShot(iCanSeeYou);
    }

    public void OnStartHover()
    {
        throw new System.NotImplementedException();
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
