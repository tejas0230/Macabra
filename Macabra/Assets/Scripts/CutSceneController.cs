using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public GameObject Player;
    public Camera Cam1;
    public Camera Cam2;
    public Camera Cam3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activatePlayer()
    {
        Player.SetActive(true);
        Cam1.gameObject.SetActive(false);
        Cam2.gameObject.SetActive(false);
        Cam3.gameObject.SetActive(false);
    }
}
