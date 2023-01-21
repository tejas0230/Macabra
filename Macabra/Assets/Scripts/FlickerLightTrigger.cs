using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLightTrigger : MonoBehaviour
{

    public List<Light> acutalLights = new List<Light>();
    public LightTriggerType triggerType;
    public GameObject otherTrigger;
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
        if(other.gameObject.CompareTag("Player"))
        {
            if(triggerType == LightTriggerType.flicker)
            {
                foreach (Light l in acutalLights)
                {
                    l.GetComponent<LightBulb>().FlickerLights(2);
                }
            }
            else
            {
                InventoryManager.instance.canTurnLightsOn = false;
                foreach (Light l in acutalLights)
                {
                    l.GetComponent<LightBulb>().SwitchLightOff();
                }
                this.gameObject.SetActive(false);
                otherTrigger.SetActive(false);
            }
            
        }
    }
}

public enum LightTriggerType
{
    switchOff,
    flicker
}
