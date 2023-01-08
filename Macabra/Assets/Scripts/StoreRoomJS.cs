using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRoomJS : MonoBehaviour
{
    public GameObject child;
    public Collider jsTrigger;
    public GameObject childFaceLight;
    public InventoryItem fuseItem;
    int fuseCount;
    public GameObject childBoneFace;
    bool isSoundPlayed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fuseCount = InventoryManager.instance.count(fuseItem);
        if(fuseCount==2)
        {
            child.SetActive(true);
            jsTrigger.enabled = true;
            childFaceLight.SetActive(true);
            
        }
    }

    private bool isVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach( var plane in planes)
        {
            if(plane.GetDistanceToPoint(point)<0)
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            childBoneFace.transform.LookAt(other.transform.GetChild(0).transform, Vector3.up);
            /*Vector3 viewPos = other.GetComponentInChildren<Camera>().WorldToViewportPoint(child.transform.position);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                AudioManager.instance.Play("Stinger1");
            }*/
            if(isVisible(other.GetComponentInChildren<Camera>(),child))
            {
                if (!isSoundPlayed)
                {
                    AudioManager.instance.Play("storeRoom");
                    isSoundPlayed = true;
                }
            }
            /*if(child.transform.GetChild(0).GetComponent<Renderer>().isVisible)
            {
                if(!isSoundPlayed)
                {
                    AudioManager.instance.Play("Stinger1");
                    isSoundPlayed = true;
                }
            }*/
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            child.transform.Translate(new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z+12));
            AudioManager.instance.Play("storeRoomStinger");
        }
    }
}
