using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour, IInteractable
{
    private Animator doorAnimator;
    [SerializeField] private Image dotCrosshair;
    [SerializeField] private Image handCrosshair;
    public float MaxRange => 1.5f;
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isOpen = true;
    [SerializeField] private float lerpSpeed;

    [SerializeField]public AudioSource doorSource;
    [SerializeField] public AudioClip doorOpen;
    [SerializeField] public AudioClip doopClose;
    private void Awake()
    {
        /*doorAnimator = GetComponent<Animator>();*/
        doorSource = GetComponent<AudioSource>();
    }
    public void OnEndHover()
    {
        dotCrosshair.gameObject.SetActive(true);
        handCrosshair.gameObject.SetActive(false);
    }

    public void OnInteract()
    {
        if(isLocked)
        {
            return;
        }
        else
        {
            if(isOpen)
            {
                Debug.Log("inside");
                StartCoroutine(CloseDoor());
                
            }
            else
            {
                StartCoroutine(OpenDoor());
                
            }
        }
    }

    IEnumerator CloseDoor()
    {
        for (int i = 0; i < 60; i++)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*lerpSpeed);
            yield return new WaitForEndOfFrame();
            if(i==30)
            {
                doorSource.clip = doopClose;
                doorSource.Play();
            }
        }
        
        isOpen = false;
    }
    IEnumerator OpenDoor()
    {
        doorSource.clip = doorOpen;
        doorSource.Play();
        for (int i = 0; i < 60; i++)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, -105, 0), Time.deltaTime*lerpSpeed);
            yield return new WaitForEndOfFrame();

        }
        
        isOpen = true;
    }
    public void OnStartHover()
    {
        dotCrosshair.gameObject.SetActive(false);
        handCrosshair.gameObject.SetActive(true);
    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
