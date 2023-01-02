using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{

    public Camera playerCam;
    public LayerMask inventoryItemLayer;
    Ray ray;
    private GameObject currentItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CastRayAlways();
        if(currentItem!= null)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                InventoryManager.instance.items.Add(currentItem.gameObject.name);
                Destroy(currentItem);
            }
        }
    }

    void CastRayAlways()
    {
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 2, inventoryItemLayer))
        {
            currentItem = hit.collider.gameObject;
            
        }
        else
        {
            currentItem = null;
            
        }
    }
}
