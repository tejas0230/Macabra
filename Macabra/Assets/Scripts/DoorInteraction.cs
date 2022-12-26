using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoorInteraction : MonoBehaviour
{

    [Header("Functional Options")]
    public bool isLocked = false;
    [SerializeField]
    bool closeDoor = false;
    public bool isInteracting = false;
    [SerializeField]
    bool isEnter = false;

    [Header("References")]
    [SerializeField]
    private PlayerController control;
    public GameObject Door;

    [Header("Functional Parameters")]
    [SerializeField]
    float speed = 10f;
    
    [Header("Standard Variables")]
    Vector3 direction;
    JointLimits limits;
    HingeJoint doorJoint;

    private void Awake()
    {
        doorJoint = Door.GetComponent<HingeJoint>();
        limits = doorJoint.limits;
        control = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (isEnter && Input.GetMouseButton(0))
        {
            isInteracting = true;
        }

        if (isEnter && Input.GetMouseButtonUp(0))
        {
            isInteracting = false;
            control.CanRotateCam = true;
            Door.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        if (isLocked)
        {
            limits.min = 0;
            doorJoint.limits = limits;
        }
        else
        {
            limits.min = -90;
            doorJoint.limits = limits;
        }

    }
    private void FixedUpdate()
    {
        if (closeDoor)
        {
            CloseDoor();
            StartCoroutine(ResetCloseDoor());
        }
        if(isInteracting)
        {
            if (isLocked)
            {
                return;
            }
            else
            {
                float input = Input.GetAxis("Mouse X");
                Mathf.Clamp(input, -1, 1);
                control.CanRotateCam = false;
                if (Input.GetAxis("Mouse X") > 0)
                {
                    Door.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Mathf.Sign(Vector3.Dot(transform.forward, direction)) * 10, 0), ForceMode.Impulse);
                }

                else if (Input.GetAxis("Mouse X") < 0)
                {
                    Door.GetComponent<Rigidbody>().AddTorque(new Vector3(0, -Mathf.Sign(Vector3.Dot(transform.forward, direction)) * 10, 0), ForceMode.Impulse);
                }
            }
        }
       
            
       
        /*if (isInteracting)
        {
            

        }
        else
        {
            control.CanRotateCam = true;
            Door.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }*/
    }




    public void CloseDoor()
    {
        Door.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 100, 0), ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = true;
        }  
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            direction = transform.position - other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = false;
            isInteracting = false;
            control.CanRotateCam = true;
        }
    }

    IEnumerator ResetCloseDoor()
    {
        yield return new WaitForSeconds(0.5f);
        closeDoor = false;
    }
}
