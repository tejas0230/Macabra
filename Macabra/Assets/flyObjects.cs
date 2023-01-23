using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class flyObjects : MonoBehaviour
{
   //public List<Rigidbody> objectsInRadius = new List<Rigidbody>();
    public List<Transform> objectsInRadius = new List<Transform>();
    float speed = 1f;
    public float amplitude = 0.1f;
    public float verticalCenter = 0f;
    public LayerMask flying;

    bool isPlayerEntered = false;

    public GameObject finalCam;

    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    private Vector3 originalPos;
    private Quaternion origRot;

    public LightBulb lig;

    // Start is called before the first frame update
    void Start()
    {
        
            float radius = 5f;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius,flying);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                //objectsInRadius.Add(hitColliders[i].GetComponent<Rigidbody>());
                objectsInRadius.Add(hitColliders[i].transform);
        }
        originalPos = finalCam.transform.localPosition;
        origRot = finalCam.transform.localRotation;
    }

    
    void Update()
    {
        if (isPlayerEntered)
        {
            float time = Time.time;
            if(time == 10)
            {
                SceneController.instance.FadeToLevel(3);
            }
            for (int i = 0; i < objectsInRadius.Count; i++)
            {
                float xRotation = Random.Range(0f, 360f) * Time.deltaTime;
                float yRotation = Random.Range(0f, 360f) * Time.deltaTime;
                float zRotation = Random.Range(0f, 360f) * Time.deltaTime;
                Quaternion targetRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
                //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);

                objectsInRadius[i].position = Vector3.Lerp(objectsInRadius[i].position, new Vector3(objectsInRadius[i].position.x, verticalCenter, objectsInRadius[i].position.z), Time.deltaTime * 0.2f);

                if (Mathf.Abs(objectsInRadius[i].position.y - verticalCenter) < 0.2f)
                {
                    objectsInRadius[i].position = new Vector3(objectsInRadius[i].position.x, verticalCenter + amplitude * Mathf.Sin(time * speed), objectsInRadius[i].position.z);
                }
                objectsInRadius[i].rotation = Quaternion.Lerp(objectsInRadius[i].rotation, targetRotation, Time.deltaTime);
                objectsInRadius[i].RotateAround(transform.position, Vector3.up, 40f * Time.deltaTime);

            }

            finalCam.transform.position = Vector3.Lerp(finalCam.transform.position, originalPos, 0.5f * Time.deltaTime);
            finalCam.transform.rotation = Quaternion.Slerp(finalCam.transform.rotation, origRot, 0.5f* Time.deltaTime);

            if (shakeDuration > 0)
            {
                finalCam.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                finalCam.transform.localPosition = originalPos;
            }
        }
        
    }

    public void ShakeCamera(float duration, float amount)
    {
        shakeDuration = duration;
        shakeAmount = amount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            finalCam.transform.position = other.GetComponentInChildren<Camera>().transform.position;
            finalCam.transform.rotation = other.GetComponentInChildren<Camera>().transform.rotation;
            other.gameObject.SetActive(false);
            finalCam.SetActive(true);
            lig.FlickerLight();
            ShakeCamera(100, 0.01f);
            isPlayerEntered = true;
            AudioManager.instance.Play("finalClimax");
            StartCoroutine(changeScene());
        }
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(9);
        SceneController.instance.FadeToLevel(3);
    }
}
