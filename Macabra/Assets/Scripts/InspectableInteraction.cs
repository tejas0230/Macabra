using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InspectableInteraction : MonoBehaviour
{
    Vector3 Initial_position;
    Quaternion Rot;

    public LayerMask inspectableLayer;

    public FPSController control;
    public Transform InspectZone;

    public static GameObject currentObject = null;
    private Rigidbody rb;

    bool isInspecting = false;
    public float Speed = 10f;
    Collider obj;
    public Camera playerCam;
    Ray ray;
    private void Start()
    {

    }

    private void Update()
    {
        RaycastHit info;
        ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out info, 2f, inspectableLayer) || currentObject != null)
        {
            Debug.DrawLine(ray.origin, info.point, Color.green);
            if (Input.GetMouseButtonDown(0) && !isInspecting)
            {
                StopAllCoroutines();
                currentObject = info.collider.gameObject;
                rb = currentObject.GetComponent<Rigidbody>();
                obj = currentObject.GetComponent<Collider>();
                Initial_position = info.collider.transform.position;
                Rot = Quaternion.Euler(info.collider.transform.localEulerAngles);
                if (rb != null)
                {
                    rb.isKinematic = true;
                    obj.enabled = false;
                }

                control.CanRotateCam = false;
                control.CanMove = false;
                StartCoroutine(MoveToTarget(currentObject, InspectZone.position, 0.8f));
                isInspecting = true;
            }
            if (isInspecting && Input.GetMouseButtonDown(1))
            {
                StopAllCoroutines();
                //currentObject.transform.rotation =  Quaternion.Euler(Rot.eulerAngles);
                //currentObject.transform.rotation = Quaternion.Slerp(currentObject.transform.rotation, Rot, 0.5f);
                StartCoroutine(rotateCorrect(currentObject, Rot));
                StartCoroutine(MoveToTarget(currentObject, Initial_position, Time.deltaTime * 100f));
                isInspecting = false;
                control.CanMove = true;
                control.CanRotateCam = true;
                if (rb != null)
                {
                    StartCoroutine(TogglePhysics(rb, true, 2f,obj));
                }
                currentObject = null;
                rb = null;
                obj = null;
                Initial_position = Vector3.zero;
                Rot = Quaternion.Euler(0,0,0);
            }
        }

        if (isInspecting)
        {
            float x = Input.GetAxis("Mouse X") * Speed;
            float y = Input.GetAxis("Mouse Y") * Speed;
            currentObject.transform.Rotate(-Vector3.up * x, Space.World);
            currentObject.transform.Rotate(-Vector3.forward * y, Space.World);
        }
    }

    IEnumerator MoveToTarget(GameObject MovedObj, Vector3 Target, float speed)
    {
        while (MovedObj.transform.position != Target)
        {
            MovedObj.transform.position = Vector3.MoveTowards(MovedObj.transform.position, Target, Time.deltaTime * speed);
            yield return null;
        }
    }
    IEnumerator rotateCorrect(GameObject moved, Quaternion rot)
    {
        while(moved.transform.rotation!= rot)
        {
            moved.transform.rotation = Quaternion.Slerp(moved.transform.rotation, rot, 10f*Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator TogglePhysics(Rigidbody rb, bool value, float TimeWait,Collider obj)
    {
        yield return new WaitForSeconds(TimeWait);
        obj.enabled = true;
        rb.isKinematic = !value;

    }
}
