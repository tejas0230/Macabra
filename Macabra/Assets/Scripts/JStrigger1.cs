using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JStrigger1 : MonoBehaviour
{

    public GameObject babyModel;
    public Light BasementLight;
    public DoorProperties doorToShut;
    public Light Flash;
    float jumpScareTime = 0.2f;
    float currentime = 0;
    float intensity = 0;
    public GameObject decals;
    public GameObject JS2;
    // Start is called before the first frame update
    void Start()
    {
        intensity = BasementLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.Play("Stinger1");
            BasementLight.GetComponent<LightBulb>().FlickerLights();
            
            //other.gameObject.GetComponent<FPSController>().CanMove = false;
            StartCoroutine(jumpScare1(other));
            //other.gameObject.GetComponent<FPSController>().CanMove = true;
            decals.SetActive(true);
            JS2.SetActive(true);
        }
    }

    IEnumerator jumpScare1(Collider other)
    {
        
        while(currentime<=jumpScareTime)
        {
            
            babyModel.SetActive(true);
            babyModel.transform.position = Vector3.MoveTowards(babyModel.transform.position, other.transform.position,15*Time.deltaTime);
            babyModel.transform.LookAt(other.transform,Vector3.up);
            yield return new WaitForSeconds((float)Random.Range(0.01f, 0.04f));
            babyModel.SetActive(false);
            currentime += Time.deltaTime;

        }
        BasementLight.GetComponent<LightBulb>().SwitchLightOff();
        BasementLight.intensity = intensity;
        
        doorToShut.CloseDoorSlam();
        babyModel.SetActive(false);
        gameObject.SetActive(false);
        Flash.enabled = false;
        //AudioManager.instance.Play("");
    }
}
