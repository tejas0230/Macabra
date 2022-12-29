using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProperties : MonoBehaviour
{
    public bool isHorizontal;
    public bool isLocked;
    public float minLimit;
    public float maxLimit;

    public bool closeDoor;
    public bool isInteracting;
    public bool isReversed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
        if(closeDoor)
        {
            StartCoroutine( CloseDoor());
        }
    }

   
    

    IEnumerator CloseDoor()
    {
        while (transform.localRotation != Quaternion.Euler(0, 0, 0))
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 0), 2 * Time.deltaTime);
            yield return null;
        }
    }
    
}
