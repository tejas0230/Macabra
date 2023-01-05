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

    public float MaxRange => 5;

    public void OnStartHover()
    {
        Debug.Log("Can turn on/of");
    }

    public void OnInteract()
    {
        Light.Invoke();       
    }

    

}
