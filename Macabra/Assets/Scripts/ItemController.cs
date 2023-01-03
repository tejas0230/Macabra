using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{

    public InventoryItem item;

    public float MaxRange => 2;

    public void OnEndHover()
    {
        print("OnEndHove");
    }

    public void OnInteract()
    {
        InventoryManager.instance.addToInventory(item);
        Destroy(gameObject);
    }

    public void OnStartHover()
    {
        print("OnStartHove");
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
