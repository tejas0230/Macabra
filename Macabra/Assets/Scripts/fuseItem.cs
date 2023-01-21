using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class fuseItem : MonoBehaviour,IInteractable

{
    public float MaxRange => 2;
    public GameObject fuse1;
    public GameObject fuse2;
    public Animator fuseAnimator;
    public List<Light> allLights = new List<Light>();
    public GameObject crosshair;
    public GameObject hand;
    public InventoryItem fuseKey;
    public GameObject fusePanel;
    public TMP_Text fuseText;
    public GameObject jumpScare1Trigger;
    public GameObject obj2completeTrigger;
    bool lightTurnedOn = false;
    public void OnEndHover()
    {
        hand.SetActive(false);
        crosshair.SetActive(true);
        fusePanel.SetActive(false);
    }
    
    public void OnInteract()
    {
        if(InventoryManager.instance.items.Contains(fuseKey))
        {
            fuseAnimator.SetBool("open", !fuseAnimator.GetBool("open"));
        }
        else
        {
            fuseText.text = "You need the Fusebox Key";
            fusePanel.SetActive(true);
        }
        
    }

    public void OnStartHover()
    {
        
        crosshair.SetActive(false);
        hand.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fuse1.activeInHierarchy && fuse2.activeInHierarchy && !lightTurnedOn)
        {

            foreach (Light l in allLights)
            {
                l.GetComponent<LightBulb>().SwitchLightOn();
            }
            InventoryManager.instance.canTurnLightsOn = true;
            lightTurnedOn = true;
            jumpScare1Trigger.SetActive(true);
            obj2completeTrigger.SetActive(true);
        }
    }

}
