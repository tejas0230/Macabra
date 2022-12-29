using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : MonoBehaviour
{
    public GameObject Player;
    public Camera Cam1;
    public Camera Cam2;
    public Camera Cam3;
    public PlayableDirector intro;
    // Start is called before the first frame update
    float timeToSkip = 3f;
    float timeElapsed = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.Space))
        {
            timeElapsed += Time.deltaTime;
            print(timeElapsed);
            if(timeElapsed==timeToSkip)
            {
                skipCutscene();
                timeElapsed = 0;
            }
        }
       else
        {
            timeElapsed = 0;
        }
    }

    public void activatePlayer()
    {
        Player.SetActive(true);
        Cam1.gameObject.SetActive(false);
        Cam2.gameObject.SetActive(false);
        Cam3.gameObject.SetActive(false);
    }

    void skipCutscene()
    {
        intro.time = 20f;
    }
}
