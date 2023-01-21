using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public LayerMask flashLayer;
    public bool isFlashHeld;
    GameObject flashLight;
    public Transform flashLocation;
    Ray ray;
    public Camera playerCam;
    AudioSource flashSource;
    public AudioClip flashon;
    public AudioClip flashof;
    public Light actualLight;
    bool isOn = false;
    public bool canTurnOn = true;
    public bool lightOftriggerIsActive = false;
    public GameObject LightOffTrigger;
    public GameObject lightoffTrigger2;
    private void Update()
    {
        if (!isFlashHeld)
            CastRayAlways();

        if(flashLight!=null && !isFlashHeld)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                flashLight.transform.SetParent(flashLocation);
                flashLight.transform.localPosition = Vector3.zero; 
                flashLight.transform.localRotation= Quaternion.Euler(0, 0, 0);
                flashSource = flashLight.GetComponent<AudioSource>();
                flashLight.GetComponent<CapsuleCollider>().enabled = false;
                flashSource.PlayOneShot(flashof);
                isFlashHeld = true;
                actualLight = flashLight.GetComponentInChildren<Light>();
            }
        }

        if(isFlashHeld)
        {
            if(!lightOftriggerIsActive)
            {
                LightOffTrigger.SetActive(true);
                lightoffTrigger2.SetActive(true);
                lightOftriggerIsActive = true;
            }
            if(Input.GetKeyDown(KeyCode.F) && canTurnOn)
            {
                if(isOn)
                {
                    actualLight.enabled = false;
                    flashSource.PlayOneShot(flashof);
                    isOn = !isOn;
                }
                else
                {
                    actualLight.enabled = true;
                    flashSource.PlayOneShot(flashon);

                    isOn = !isOn;
                }
                
            }
            
        }
    }
    void CastRayAlways()
    {
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 2, flashLayer))
        {
            flashLight = hit.collider.gameObject;   
        }
        else
        {
            flashLight = null;
        }
        
    }


    IEnumerator MoveToTarget(GameObject MovedObj, Vector3 Target, float speed)
    {
        while (MovedObj.transform.position != Target)
        {
            MovedObj.transform.position = Vector3.MoveTowards(MovedObj.transform.position, Target, Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator rotateCorrect(GameObject moved, Quaternion rot)
    {
        while (moved.transform.rotation != rot)
        {
            moved.transform.rotation = Quaternion.Slerp(moved.transform.rotation, rot, 10f * Time.deltaTime);
            yield return null;
        }
    }

    public void turnFlashOff()
    {
        if(flashLight!=null)
        {
            actualLight.enabled = false;
            flashSource.PlayOneShot(flashof);
            isOn = !isOn;
        }
    }
}
