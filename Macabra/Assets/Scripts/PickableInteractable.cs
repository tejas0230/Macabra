using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrabObjectClass
{
    public bool m_FreezeRotation;
    public float m_PickupRange = 3f;
    public float m_ThrowStrength = 50f;
    public float m_distance = 3f;
    public float m_maxDistanceGrab = 4f;
}
public class PickableInteractable : MonoBehaviour
{
    public LayerMask PickableLayer;
    public GrabObjectClass objectGrab = new GrabObjectClass();

    private float PickupRange = 3f;
    private float ThrowStrength = 50f;
    private float distance = 3f;
    private float maxDistanceGrab = 4f;

    private Ray playerAim;
    private GameObject objectHeld;
    private bool isObjectHeld;
    private bool tryPickupObject;

    void Start()
    {
        isObjectHeld = false;
        tryPickupObject = false;
        objectHeld = null;
    }

	void FixedUpdate()
	{
		if (Input.GetMouseButton(0))
		{
			if (!isObjectHeld)
			{
				tryPickObject();
				tryPickupObject = true;
			}
			else
			{
				holdObject();
			}
		}
		else if (isObjectHeld)
		{
			DropObject();
		}

		if (Input.GetMouseButtonUp(0) && isObjectHeld)
		{
			isObjectHeld = false;
			objectHeld.GetComponent<Rigidbody>().useGravity = true;
			ThrowObject();
		}

	}

	void tryPickObject()
    {
		Ray playerAim = gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;
		if (Physics.Raycast(playerAim, out hit, PickupRange,PickableLayer))
        {
			objectHeld = hit.collider.gameObject;
			if(tryPickupObject)
            {
				isObjectHeld = true;
				objectHeld.GetComponent<Rigidbody>().useGravity = false;
				if(objectGrab.m_FreezeRotation)
                {
					objectHeld.GetComponent<Rigidbody>().freezeRotation = true;
                }
				else
                {
					objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
				}
				PickupRange = objectGrab.m_PickupRange;
				ThrowStrength = objectGrab.m_ThrowStrength;
				distance = objectGrab.m_distance;
				maxDistanceGrab = objectGrab.m_maxDistanceGrab;
			}
        }
	}

	private void holdObject()
	{
		Ray playerAim =gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

		Vector3 nextPos = gameObject.transform.position + playerAim.direction * distance;
		Vector3 currPos = objectHeld.transform.position;

		objectHeld.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;

		if (Vector3.Distance(objectHeld.transform.position, gameObject.transform.position) > maxDistanceGrab)
		{
			DropObject();
		}
	}

	private void DropObject()
	{
		isObjectHeld = false;
		tryPickupObject = false;
		objectHeld.GetComponent<Rigidbody>().useGravity = true;
		objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
		objectHeld = null;
	}
	private void ThrowObject()
	{
		objectHeld.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * ThrowStrength);
		objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
		objectHeld = null;
	}
}
