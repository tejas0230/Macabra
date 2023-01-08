using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb: MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private AudioSource lightSource;
    [SerializeField]
    private AudioClip lightOn;
    [SerializeField]
    private AudioClip lightOff;
    [SerializeField]
    bool isOn;
    private Light actualLight;
    // Start is called before the first frame update
    public GameObject swithcOn;
    public GameObject swithcOff;

    public GameObject glass;
    void Start()
    {
        actualLight = gameObject.GetComponent<Light>();
        
        //SwitchLight();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            if(swithcOff!=null && swithcOn!=null)
            {
                swithcOff.SetActive(false);
                swithcOn.SetActive(true);
            }
            
           
        }
        else
        {
            if (swithcOff != null && swithcOn != null)
            {
                swithcOff.SetActive(true);
                swithcOn.SetActive(false);
            }
        }
    }

    public void SwitchLight()
    {
        if (isOn)
        {
            actualLight.enabled = false;
            isOn = !isOn;
            if(glass!=null)
                glass.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            lightSource.PlayOneShot(lightOff);
        }
        else
        {
            actualLight.enabled = true;
            isOn = !isOn;
            if (glass != null)
                glass.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
            lightSource.PlayOneShot(lightOn);
        } 
    }

    public void SwitchLightOff()
    {
        actualLight.enabled = false;
        isOn = false;
        glass.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
        lightSource.PlayOneShot(lightOff);
    }
    public void SwitchLightOn()
    {
        actualLight.enabled = true;
        isOn = true;
        glass.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
        lightSource.PlayOneShot(lightOn);
    }

    IEnumerator Flicker()
    {
        while(isOn!=false)
        {
            gameObject.GetComponent<Light>().intensity = (float)Random.Range(0, 60);
            yield return new WaitForSecondsRealtime((float)Random.Range(0.01f,0.1f));
        }
    }
    public void FlickerLights()
    {
        StartCoroutine(Flicker());
    }

    public void StopFlickering()
    {
        isOn = false;
        
    }
}
