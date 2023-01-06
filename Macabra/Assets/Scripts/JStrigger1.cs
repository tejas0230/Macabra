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
            other.gameObject.GetComponent<FPSController>().CanMove = false;
            StartCoroutine(jumpScare1());
            other.gameObject.GetComponent<FPSController>().CanMove = true;

        }
    }

    IEnumerator jumpScare1()
    {
        
        while(currentime<=jumpScareTime)
        {
            babyModel.SetActive(true);
            yield return new WaitForSeconds((float)Random.Range(0.01f, 0.04f));
            babyModel.SetActive(false);
            currentime += Time.deltaTime;
        }
        BasementLight.GetComponent<LightBulb>().SwitchLightOff();
        BasementLight.intensity = intensity;
        Flash.enabled = false;
        doorToShut.CloseDoorSlam();
        babyModel.SetActive(false);
        gameObject.SetActive(false);
       //AudioManager.instance.Play("");
    }
}
