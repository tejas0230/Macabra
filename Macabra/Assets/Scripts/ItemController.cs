using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{

    public InventoryItem item;
    public GameObject crosshair;
    public GameObject hand;
    public float MaxRange => 2;

    public void OnEndHover()
    {
        hand.SetActive(false);
        crosshair.SetActive(true);
    }

    public void OnInteract()
    {
        InventoryManager.instance.addToInventory(item);
        Destroy(gameObject);
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
        
    }
}
