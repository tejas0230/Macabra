using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public FPSController controls;
    public LayerMask doorLayer;
    public Camera playerCam;
    Ray ray;
    bool isDoorHeld;
    GameObject door= null;
    DoorProperties doorProperties = null;

    float rotX;
    bool doorFound = false;

    public GameObject doorUnlockedPanel;
    public GameObject doorLockedPanel;
    public TMP_Text keyText;
    private void Update()
    {
        if (!isDoorHeld)
            CastRayAlways();

        if (Input.GetButton("Fire1") && door != null)
        {
            isDoorHeld = true;
            
            controls.CanRotateCam = false;
            
        }
        else
        {
            isDoorHeld = false;
            if (doorProperties != null)
            {
                
                doorProperties.isDoorHeld = false;
            }
            controls.CanRotateCam = true;
            
        }

        if (isDoorHeld)
        {
            doorLockedPanel.SetActive(false);
            doorUnlockedPanel.SetActive(false);
            doorProperties.isDoorHeld = true;
            if (doorProperties.isLocked)
                return;
            
            if (Vector3.Dot(door.transform.parent.gameObject.transform.forward, door.transform.parent.gameObject.transform.position - transform.position) > 0)
            {
                print("in Front");
                if (doorProperties.isReversed)
                    rotX -= Input.GetAxis("Mouse X");
                else
                    rotX += Input.GetAxis("Mouse X");
            }
            else if (Vector3.Dot(door.transform.parent.gameObject.transform.forward, door.transform.parent.gameObject.transform.position - transform.position) < 0)
            {
                print("in back");
                if (doorProperties.isReversed)
                    rotX += Input.GetAxis("Mouse X");
                else
                    rotX -= Input.GetAxis("Mouse X");
            } 
            rotX = Mathf.Clamp(rotX, doorProperties.minLimit, doorProperties.maxLimit);
            //print(rotX);
            door.transform.localRotation = Quaternion.Slerp(door.transform.localRotation, Quaternion.Euler(0, rotX, 0), 10 * Time.deltaTime);
        }
    }

    void CastRayAlways()
    {
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 2, doorLayer))
        {
            door = hit.collider.gameObject;
            doorProperties = door.GetComponent<DoorProperties>();
            if(doorProperties.isLocked)
            {
                keyText.text = "You need the " + doorProperties.requiredKey.itemName + " to unlock.";
                doorLockedPanel.SetActive(true);
            }
            else
            {
                doorUnlockedPanel.SetActive(true);
            }
            doorProperties.isInteracting = true;
            doorFound = true;
        }
        else
        {
            if(doorFound)
            {
                if(doorProperties!=null)
                {
                    doorProperties.isInteracting = false;
                    doorProperties.isDoorHeld = false;
                }
                doorLockedPanel.SetActive(false);
                doorUnlockedPanel.SetActive(false);
                doorProperties = null;
                door = null;
            }
            
        }
    }
}
