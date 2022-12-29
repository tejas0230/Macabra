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
    void Start()
    {
        actualLight = gameObject.GetComponent<Light>();
        //SwitchLight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchLight()
    {
        if (isOn)
        {
            actualLight.enabled = false;
            isOn = !isOn;
            
                lightSource.PlayOneShot(lightOff);
        }
        else
        {
            actualLight.enabled = true;
            isOn = !isOn;
            
                lightSource.PlayOneShot(lightOn);
        } 
    }

    public void SwitchLightOff()
    {
        actualLight.enabled = false;
        isOn = false;
        
            lightSource.PlayOneShot(lightOff);
    }

    IEnumerator Flicker()
    {
        while(isOn!=false)
        {
            gameObject.GetComponent<Light>().intensity = (float)Random.Range(0, 5);
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }
    public void FlickerLights()
    {
        StartCoroutine(Flicker());
    }
}
