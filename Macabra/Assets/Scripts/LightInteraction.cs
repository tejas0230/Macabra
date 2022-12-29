using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteraction : MonoBehaviour
{
    public LayerMask lightLayer;
    public Camera playerCam;
    Ray ray;
    GameObject lightButton = null;
    LightSwitch lightSwitch = null;

    private void Update()
    {
        CastRayAlways();
        if(lightButton!=null)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                lightSwitch.OnInteract();
            }
        }
    }

    void CastRayAlways()
    {
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 2, lightLayer))
        {
            lightButton = hit.collider.gameObject;
            lightSwitch = lightButton.GetComponent<LightSwitch>();
        }
        else
        {
            lightButton = null;
            lightSwitch = null;

        }
    }
}
