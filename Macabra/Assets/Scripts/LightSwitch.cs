using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LightSwitch: MonoBehaviour, IInteractable
{
    public UnityEvent Light;
    // Start is called before the first frame update
   

    public float MaxRange => 2;

    public void OnStartHover()
    {
        Debug.Log("Can turn on/of");
    }

    public void OnInteract()
    {
        Light.Invoke();
    }

    public void OnEndHover()
    {
        Debug.Log("Cant");
    }

   
}
