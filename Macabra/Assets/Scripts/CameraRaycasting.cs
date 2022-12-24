using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycasting : MonoBehaviour
{
    
    [SerializeField] private float range;
    public IInteractable currentTarget;
    private Camera mainCam;


    public int speed;
    public float friction;
    public float lerpSpeed;
    public float xDeg;
    public float yDeg;
    public Quaternion fromRotation;
    public Quaternion toRotation;

    [SerializeField] private Image dotCrosshair;
    [SerializeField] private Image handCrosshair;

    private GameObject CurrentInteractable;
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(currentTarget!= null)
            {
                currentTarget.OnInteract();
            }
            
        }
        /*Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1))
        {
            if (hit.collider.tag == "Door")
            {
                dotCrosshair.gameObject.SetActive(false);
                handCrosshair.gameObject.SetActive(true);
                if (Input.GetMouseButton(0))
                {
                    if(hit.collider.GetComponent<door>().isLocked)
                    {
                        return;
                    }
                    else
                    {
                        if(hit.transform.rotation.y==0)
                        {
                            hit.collider.GetComponent<door>().doorSource.clip = hit.collider.GetComponent<door>().doorOpen;
                            hit.collider.GetComponent<door>().doorSource.Play();
                        }
                        xDeg -= Input.GetAxis("Mouse X") * speed * friction;
                        //yDeg += Input.GetAxis("Mouse Y") * speed * friction;
                        xDeg = Mathf.Clamp(xDeg, 0, 105);
                        fromRotation = hit.transform.rotation;
                        toRotation = Quaternion.Euler(0, -xDeg, 0);
                        hit.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);

                    }
                }
            }
            else
            {
                dotCrosshair.gameObject.SetActive(true);
                handCrosshair.gameObject.SetActive(false);
            }
        }*/
    }

    private void RaycastForInteractable()
    {
        RaycastHit whatIHit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out whatIHit,range))
        {
            IInteractable interactable = whatIHit.collider.GetComponent<IInteractable>();
            if(interactable!=null)
            {
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
