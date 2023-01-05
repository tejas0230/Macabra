using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Interior2Timeline : MonoBehaviour
{
    public static Interior2Timeline instance;
    public PlayableDirector int2Timeline;
    public PlayableAsset obj1Complete;
    public FPSController player;
    public AudioSource phone;
    public Collider phoneCollider;

    public GameObject globalVolume;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(PlayableAsset obj)
    {
        int2Timeline.playableAsset = obj;
        int2Timeline.Play();
        player.CanMove = false;
    }


    public void resetPlayer()
    {
        player.CanMove = true;
        phone.Play();
        phoneCollider.enabled = true;
    }


    public void changePost()
    {
        globalVolume.SetActive(true);
    }
}
