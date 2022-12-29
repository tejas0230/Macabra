using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    public FPSController controls;
    public LayerMask drawerLayer;
    public Camera playerCam;
    Ray ray;
    bool isDrawerHeld;
    GameObject drawer = null;
    DrawerProperties drawerProperties = null;
    bool drawerFound = false;

    public float slideY = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrawerHeld)
            CastRayAlways();

        if (Input.GetButton("Fire1") && drawer != null)
        {
            isDrawerHeld = true;

            controls.CanRotateCam = false;

        }
        else
        {
            isDrawerHeld = false;
            if (drawerProperties != null)
            {

                drawerProperties.isDrawerHeld = false;
            }
            controls.CanRotateCam = true;

        }

        if (isDrawerHeld)
        {
            drawerProperties.isDrawerHeld = true;
            if (drawerProperties.isLocked)
                return;

            if (Input.GetAxis("Mouse Y") > 0 && drawer.transform.position.x > drawerProperties.minLimit) 
            {
                drawer.transform.position += Vector3.left*slideY* Time.deltaTime;
            }
            else if(Input.GetAxis("Mouse Y") < 0 && drawer.transform.position.x < drawerProperties.maxLimit)
            {
                drawer.transform.position += Vector3.right * slideY * Time.deltaTime;
            }
            /*slideY -= Input.GetAxis("Mouse Y");
            slideY = Mathf.Clamp(slideY, drawerProperties.minLimit, drawerProperties.maxLimit);*/

           /* drawer.transform.Translate(new Transform(slideY*Time.deltaTime, drawer.transform.position.y, drawer.transform.position.z));*/
            /*drawer.transform.position = Vector3.Lerp(drawer.transform.position, new Vector3(slideY, 0, 0), 10 * Time.deltaTime);*/
        }
    }

    void CastRayAlways()
    {
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 2, drawerLayer))
        {
            drawer = hit.collider.gameObject;
            drawerProperties = drawer.GetComponent<DrawerProperties>();
            drawerProperties.isInteracting = true;
            drawerFound = true;
        }
        else
        {
            if (drawerFound)
            {
                if (drawerProperties != null)
                {
                    drawerProperties.isInteracting = false;
                    drawerProperties.isDrawerHeld = false;
                }

                drawerProperties = null;
                drawer = null;
            }

        }
    }
}
