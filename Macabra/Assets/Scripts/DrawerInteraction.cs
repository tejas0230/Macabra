using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    public LayerMask drawerLayer;

    private Ray playerAim;
    private GameObject drawer;

    private bool isInteracting = false;
    private bool isDrawerHeld = false;
    [SerializeField]
    private PlayerController control;

    float speed = 0.01f;
    Rigidbody rb;
    private void Awake()
    {
        control = FindObjectOfType<PlayerController>();   
    }

    private void FixedUpdate()
    {
        RaycastHit info;
        playerAim = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(playerAim, out info, 5f, drawerLayer))
        {
            if(Input.GetMouseButton(0)&& !isInteracting)
            {
                drawer = info.collider.gameObject;
                rb = drawer.GetComponent<Rigidbody>();
                
                control.CanRotateCam = false;
                isInteracting = true;
            }
            if (isInteracting && Input.GetMouseButtonUp(0))
            {
                StopAllCoroutines();
                isInteracting = false;

                control.CanRotateCam = true;
              
            }
        }
    
        

        if (isInteracting)
        {
            float x = Input.GetAxis("Mouse Y") * speed;

            rb.AddForce(new Vector3(-x*10f, 0, 0), ForceMode.VelocityChange);
        }
    }
    void tryHoldDrawer()
    {
        
        
    }
    IEnumerator TogglePhysics(Rigidbody rb, bool value, float TimeWait)
    {
        yield return new WaitForSeconds(TimeWait);
        rb.isKinematic = !value;
    }
}
