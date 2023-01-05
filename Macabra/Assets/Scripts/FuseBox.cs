using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuseBox : MonoBehaviour,IInteractable
{
    public float MaxRange => 1;
    public InventoryItem Fuse;
    public GameObject fuse;
    public GameObject fusePanel;
    public TMP_Text fuseText;

    public GameObject crosshair;
    public GameObject hand;
    public void OnEndHover()
    {
        fusePanel.SetActive(false);
        hand.SetActive(false);
        crosshair.SetActive(true);
    }

    public void OnInteract()
    {
        if(InventoryManager.instance.items.Contains(Fuse))
        {
            fuse.SetActive(true);
            InventoryManager.instance.removeFromInventory(Fuse);
        }
        else
        {
            fuseText.text = "Something is missing";
            fusePanel.SetActive(true);
        }
        
    }

    public void OnStartHover()
    {
        crosshair.SetActive(false);
        hand.SetActive(true);
        if (InventoryManager.instance.items.Contains(Fuse))
        {
            fuseText.text = "Place Fuse";
            fusePanel.SetActive(true);
        }
       
    }

    
}
