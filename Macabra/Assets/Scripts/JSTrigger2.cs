using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSTrigger2 : MonoBehaviour
{

    public GameObject door;
    public GameObject wall;
    public GameObject wall2;
    public GameObject decalWall;
    public Collider firstCollider;
    public Collider secondCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Vector3.Dot(transform.forward,other.transform.forward)>=0)
            {
                wall2.SetActive(false);
                
            }
            else
            {
                door.GetComponentInChildren<DoorProperties>().CloseDoorSlam();
                //wall2.tag = "sfs";
                StartCoroutine(wait());
                
            }
         
        }
    }

    IEnumerator wait()
    {
        secondCollider.enabled = true;
        firstCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        wall.SetActive(true);
        door.SetActive(false);
        
    }
}
