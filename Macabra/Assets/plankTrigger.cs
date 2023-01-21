using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plankTrigger : MonoBehaviour
{
    public GameObject barricade;
    public AudioSource barricadeSource;
    public AudioClip plankClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            barricade.SetActive(true);
            barricadeSource.PlayOneShot(plankClip);
            this.gameObject.SetActive(false);
        }
    }
}
