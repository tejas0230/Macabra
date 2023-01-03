using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraRaycasting : MonoBehaviour
{
    public Camera mainCam;
    [SerializeField] private float range;
    
    public IInteractable currentTarget;

    
    Ray ray;

    public GameObject currentInteractable;
    
    
    private void Awake()
    {
        mainCam = Camera.main;
        
    }
    void Start()
    {
     
    }

   
    void Update()
    {
        RaycastForInteractable();
        if(Input.GetMouseButtonDown(0))
        {
            if(currentTarget!=null)
            {
                currentTarget.OnInteract();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
        

        
    

    private void RaycastForInteractable()
    {
        RaycastHit whatIHit;
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray,out whatIHit,range))
        {
            Debug.DrawLine(mainCam.transform.position, whatIHit.point, Color.red);
            IInteractable interactable = whatIHit.collider.GetComponent<IInteractable>();
            if(interactable!=null)
            {
                Debug.Log(whatIHit.collider.name);
                if(whatIHit.distance <= interactable.MaxRange)
                {
                    if(interactable==currentTarget)
                    {
                        return;
                    }
                    else if(currentTarget!=null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }
                    else
                    {
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }
                }
                else
                {
                    if(currentTarget!=null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                        return;
                    }
                }
            }
            else
            {

                if (currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = null;
                    return;
                }
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                return;
            }
        }
    }


    
    
}

/*

RaycastHit info;
if (Physics.Raycast(ray, out info, range))
{
    currentInteractable = info.collider.gameObject;
    if (Input.GetKeyDown(KeyCode.Q) && !isInspecting)
    {

        if (currentInteractable.tag == "Inspectable")
        {
            StopAllCoroutines();

            rb = currentInteractable.GetComponent<Rigidbody>();
            initialPosi = info.collider.transform.position;
            inspectableRot = Quaternion.Euler(info.collider.transform.localEulerAngles);
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            controls.CanRotateCam = false;
            StartCoroutine(MoveToTarget(currentInteractable, inspectZone.position, 3f));
            isInspecting = true;
        }
        //currentTarget.OnInteract();    
    }
    if (Input.GetKeyDown(KeyCode.E) && isInspecting)
    {
        StopAllCoroutines();
        currentInteractable.transform.rotation = Quaternion.Euler(inspectableRot.eulerAngles);
        StartCoroutine(MoveToTarget(currentInteractable, initialPosi, Time.deltaTime * 100f));
        isInspecting = false;
        controls.CanRotateCam = true;
        if (rb != null)
        {
            StartCoroutine(TogglePhysics(rb, true, 2f));
        }
    }
}

if (isInspecting)
{
    float x = Input.GetAxis("Mouse X") * 10;
    float y = Input.GetAxis("Mouse Y") * 10;
    currentInteractable.transform.Rotate(-Vector3.up * x, Space.World);
    currentInteractable.transform.Rotate(-Vector3.forward * y, Space.World);

}*/