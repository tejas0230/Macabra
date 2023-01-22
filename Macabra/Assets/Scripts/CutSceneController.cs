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
    float timeToSkip = 0.5f;
    float timeElapsed = 0;

    bool didSkip = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       if(Input.GetKey(KeyCode.Space)&&!didSkip)
        {
            timeElapsed += Time.deltaTime;
            print(timeElapsed);
            if(timeElapsed>=timeToSkip)
            {
                print("timeElapsed==timeToSkip");
                skipCutscene();
                didSkip = true;
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

    public void playPart1()
    {
        voiceLineManager.instance.playPart1(voiceLineManager.instance.part1);
    }
    public void skipCutscene()
    {
        print("SkipCalled");
        intro.time = 19f;
    }
}
