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
    Rigidbody rb;
    Vector3 initialPosi;
    Quaternion inspectableRot;
    public Transform inspectZone;

    public PlayerController controls;
    bool isInspecting = false ;
    private void Awake()
    {
        mainCam = Camera.main;
        controls = FindObjectOfType<PlayerController>();
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
        /*if (currentTarget != null)
        {
            if(Input.GetMouseButton(0))
            {
                inspectablePosi = currentInteractable.transform.position;
                inspectableRot = currentInteractable.transform.rotation;
                if (currentInteractable.tag == "Inspectable")
                {
                    

                    currentInteractable.GetComponent<Rigidbody>().useGravity = false;
                    currentInteractable.GetComponent<Rigidbody>().freezeRotation = true;
                    currentInteractable.transform.position = mainCam.transform.position + ray.direction * 3;
                    controls.CanRotateCam = false;
                    float rotInputX = Input.GetAxis("Mouse X");
                    float rotInputY = Input.GetAxis("Mouse Y");
                    currentInteractable.transform.rotation = Quaternion.Euler(rotInputX, rotInputY, 0);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                currentInteractable.transform.position = inspectablePosi;
                currentInteractable.transform.rotation = inspectableRot;
            }
        }*/
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


    void inspect()
    {
        currentInteractable.GetComponent<Rigidbody>().useGravity = false;
        currentInteractable.GetComponent<Rigidbody>().freezeRotation = true;
        Vector3 nextPos = mainCam.transform.position + ray.direction * 3;
        Vector3 currPos = currentInteractable.transform.position;
        
        currentInteractable.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;
    }
   
    IEnumerator MoveToTarget(GameObject MovedObject, Vector3 Target, float Speed)
    {
        while(MovedObject.transform.position!=Target)
        {
            MovedObject.transform.position = Vector3.MoveTowards(MovedObject.transform.position, Target, Time.deltaTime * Speed);
            yield return null;
        }
    }

    IEnumerator TogglePhysics(Rigidbody rb, bool value, float TimeWait)
    {
        yield return new WaitForSeconds(TimeWait);
        rb.isKinematic = !value;
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