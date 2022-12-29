using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToInterior : MonoBehaviour
{
    bool canSwitchScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&&canSwitchScene)
        {
            SceneController.instance.FadeToLevel(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            canSwitchScene = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSwitchScene = false;
        }
    }
}
