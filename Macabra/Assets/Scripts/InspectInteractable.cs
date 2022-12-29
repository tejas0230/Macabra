using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InspectInteractable : MonoBehaviour
{
    Vector3 Initial_position;
    Quaternion Rot;

    public LayerMask inspectableLayer;

    public PlayerController control;
    public Transform InspectZone;

    public static GameObject currentObject=null;
    private Rigidbody rb;

    bool isInspecting = false;
    public float Speed = 10f;
    Collider obj;
    private void Start()
    {
        
    }

    private void Update()
    {
        RaycastHit info;
        if(Physics.Raycast(transform.position,transform.forward,out info,4f,inspectableLayer)|| currentObject!=null)
        {
            Debug.DrawLine(transform.position, info.point, Color.green);
            if (Input.GetMouseButtonDown(0)&&!isInspecting)
            {
                StopAllCoroutines();
                currentObject = info.collider.gameObject;
                rb = currentObject.GetComponent<Rigidbody>();
                obj = currentObject.GetComponent<Collider>();
                Initial_position = info.collider.transform.position;
                Rot = Quaternion.Euler(info.collider.transform.localEulerAngles);
                if(rb!=null)
                {
                    rb.isKinematic = true;
                    obj.enabled = false;
                }
                
                control.CanRotateCam = false;
                StartCoroutine(MoveToTarget(currentObject, InspectZone.position, 0.8f));
                isInspecting = true;
            }
            if(isInspecting && Input.GetMouseButtonDown(1))
            {
                StopAllCoroutines();
                currentObject.transform.rotation = Quaternion.Euler(Rot.eulerAngles);
                StartCoroutine(MoveToTarget(currentObject, Initial_position, Time.deltaTime * 100f));
                isInspecting = false;
                
                control.CanRotateCam = true;
                if(rb!=null)
                {
                    StartCoroutine(TogglePhysics(rb, true, 2f));
                }
            }
        }

        if(isInspecting)
        {
            float x = Input.GetAxis("Mouse X") * Speed;
            float y = Input.GetAxis("Mouse Y") * Speed;
            currentObject.transform.Rotate(-Vector3.up * x, Space.World);
            currentObject.transform.Rotate(-Vector3.forward * y, Space.World);
        }
    }

    IEnumerator MoveToTarget(GameObject MovedObj,Vector3 Target, float speed)
    {
        while(MovedObj.transform.position!=Target)
        {
            MovedObj.transform.position = Vector3.MoveTowards(MovedObj.transform.position, Target, Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator TogglePhysics(Rigidbody rb,bool value,float TimeWait)
    {
        yield return new WaitForSeconds(TimeWait);
        obj.enabled = true;
        rb.isKinematic = !value;
        
    }
}
