using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    public FPSController controls;
    //public LayerMask drawerLayer;
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

        if(drawer!=null)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                drawer.transform.Translate(drawerProperties.FinalPos, Space.Self);
                //StartCoroutine(openDrawer());
            }
        }
    }

    void CastRayAlways()
    {
        int drawerLayer = 1 << 10;
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 2, drawerLayer))
        {
            drawer = hit.collider.gameObject;
            drawerProperties = drawer.GetComponent<DrawerProperties>();
            drawerFound = true;
        }
        else
        {

            drawerProperties = null;
                drawer = null;
            

        }
    }

    /*IEnumerator openDrawer(GameObject Drawer, DrawerProperties properties)
    {
        while(drawer.transform.position != properties.FinalPos.position)
        {
            
        }
    }*/
}
