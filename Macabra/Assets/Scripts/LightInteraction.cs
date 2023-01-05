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

    public GameObject crosshair;
    public GameObject lightBulbUI;
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
        if (Physics.Raycast(ray, out RaycastHit hit, 70, lightLayer))
        {
            crosshair.SetActive(false);
            lightBulbUI.SetActive(true);
            lightButton = hit.collider.gameObject;
            lightSwitch = lightButton.GetComponent<LightSwitch>();
        }
        else
        {
            lightBulbUI.SetActive(false);
            crosshair.SetActive(true);
            lightButton = null;
            lightSwitch = null;

        }
    }
}
