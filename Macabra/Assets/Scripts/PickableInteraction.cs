using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableInteraction : MonoBehaviour
{
    public FPSController controls;
    public Camera playerCam;
    public LayerMask pickableLayer;

    private GameObject currentPickable;
    private bool isObjectPicked = false;
    private bool isInteracting = false;
    private bool tryPickupObject = false;
    private bool isInspecting = false;
    Ray ray;

    private float inspectionTime = 1f;
    private float currentPickupTimerElapsed = 0;
    void Start()
    {
        isObjectPicked = false;
        isInteracting = false;
        tryPickupObject = false;
    }

    
    void Update()
    {
        if(!isInteracting && !isInspecting)
            CastRayAlways();
        if (isInteracting)
        {
            if(Input.GetButton("Fire1"))
            {
                isInspecting = true; 
                controls.CanMove = false;
                controls.CanRotateCam = false;
                float x = Input.GetAxis("Mouse X") * 2;
                float y = Input.GetAxis("Mouse Y") * 2;
                currentPickable.transform.Rotate(-Vector3.up * x, Space.World);
                currentPickable.transform.Rotate(-Vector3.forward * y, Space.World);
            }
            else
            {
                isInspecting = false;
                controls.CanMove = true;
                controls.CanRotateCam = true;
            }
        }

        if(!isInspecting)
        {
            if (Input.GetButtonDown("Fire1") && !isObjectPicked && currentPickable!=null)
            {
                isInteracting = true;
            }
            else if (Input.GetButtonDown("Fire2") && isObjectPicked && currentPickable != null)
            {
                isInteracting = false;
            }
        }
            
    }

    void CastRayAlways()
    {
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 2, pickableLayer))
        {
            currentPickable = hit.collider.gameObject;
         }
        else
        {
            currentPickable = null;
        }
    }
    private void FixedUpdate()
    {
        if (isInteracting)
        {
            if (!isObjectPicked)
            {
                tryPickObject();
                tryPickupObject = true;
            }
            else
            {
                holdObject();
            }
        }
        else if (isObjectPicked)
        {
            DropObject();
        }
    }
    void tryPickObject()
    {
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2, pickableLayer))
        {
            currentPickable = hit.collider.gameObject;
            if (tryPickupObject)
            {
                isObjectPicked = true;
                currentPickable.GetComponent<Rigidbody>().useGravity = false;
                currentPickable.GetComponent<Rigidbody>().freezeRotation = true;
            }
        }
        else
        {
            currentPickable = null;
        }
    }



    private void holdObject()
    {
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 nextPos = playerCam.transform.position + ray.direction * 2;
        Vector3 currPos = currentPickable.transform.position;
        currentPickable.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;
    }
    private void DropObject()
    {
        isObjectPicked = false;
        tryPickupObject = false;
        currentPickable.GetComponent<Rigidbody>().useGravity = true;
        currentPickable.GetComponent<Rigidbody>().freezeRotation = false;
        currentPickable = null;
    }
}

