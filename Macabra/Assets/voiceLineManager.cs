using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiceLineManager : MonoBehaviour
{

    public static voiceLineManager instance;
    public AudioSource voicelineSource;

    public List<AudioClip> part1 = new List<AudioClip>();
    public List<AudioClip> part2 = new List<AudioClip>();
    public List<AudioClip> part3 = new List<AudioClip>();
    // Start is called before the first frame update

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   public void playPart1(List<AudioClip> part)
    {
        StartCoroutine(playPart1Clips(part));
    }

    IEnumerator playPart1Clips(List<AudioClip> part)
    {
        foreach (AudioClip clip in part)
        {
            voicelineSource.PlayOneShot(clip);
            while (voicelineSource.isPlaying)
            {
                yield return null;
            }
        }
    }
}
