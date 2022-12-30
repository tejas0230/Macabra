using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LightSwitch: MonoBehaviour
{
     public UnityEvent Light;


    /*[Header("References")]
    [SerializeField]
    private AudioSource lightSource;
    [SerializeField]
    private AudioClip lightOn;
    [SerializeField]
    private AudioClip lightOff;
    [SerializeField]
    bool isOn;
    public Light actualLight;*/

    public float MaxRange => 2;

    public void OnStartHover()
    {
        Debug.Log("Can turn on/of");
    }

    public void OnInteract()
    {
        Light.Invoke();       
    }

    /*public void SwitchLight()
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
        while (isOn != false)
        {
            gameObject.GetComponent<Light>().intensity = (float)Random.Range(0, 5);
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }
    public void FlickerLights()
    {
        StartCoroutine(Flicker());
    }*/


}
