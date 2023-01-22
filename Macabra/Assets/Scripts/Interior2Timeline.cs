using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;
public class Interior2Timeline : MonoBehaviour
{
    public static Interior2Timeline instance;
    public PlayableDirector int2Timeline;
    public PlayableAsset obj1Complete;
    //public FPSController player;
    public GameObject player;
    public AudioSource phone;
    public Collider phoneCollider;

    public GameObject globalVolume;
    //public VolumeProfile eveningProfile;
    public VolumeProfile nightProfile;
    public Material skybox;
    
    public Light dirLight;
    
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
        player.SetActive(false);
    }


    public void resetPlayer()
    {
        player.SetActive(true);
        phone.Play();
        phoneCollider.enabled = true;
    }


    public void changePost()
    {
        globalVolume.GetComponent<Volume>().profile = nightProfile;
       
        dirLight.gameObject.SetActive(false);
        RenderSettings.skybox = skybox; 
    }
}
