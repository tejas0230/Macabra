using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ClockScare : MonoBehaviour
{
    public AudioSource clockSource;
    public AudioClip chimeSFX;
    public Collider clockScaretrigger;
    public InventoryItem officeKey;
    public AudioMixer ClockMixer;
    bool isScareDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isScareDone)
        {
            if (InventoryManager.instance.items.Contains(officeKey))
            {
                clockScaretrigger.enabled = true;
            }
        }
         
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            clockSource.Stop();
            clockSource.volume = 1;
            
            clockSource.PlayOneShot(chimeSFX);
            
            isScareDone = true;
            clockScaretrigger.enabled = false;
        }
    }
}
