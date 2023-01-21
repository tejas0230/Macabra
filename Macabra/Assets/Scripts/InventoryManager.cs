using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    //public List<string> items = new List<string>();
    public List<InventoryItem> items = new List<InventoryItem>();
    public bool canTurnLightsOn = true;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToInventory(InventoryItem item)
    {
        items.Add(item);
    }

    public void removeFromInventory(InventoryItem item)
    {
        items.Remove(item);
    }

    public int count(InventoryItem item)
    {
        int a = 0;
        foreach(InventoryItem i in items)
        {
            if (i == item)
                a++;
        }
        return a;
    }
}
