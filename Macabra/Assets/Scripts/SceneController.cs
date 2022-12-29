using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
   
    public Animator fader;
    private int levelToLoad;
    public static SceneController instance;
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
    void Update()
    {

    }
    public void FadeToLevel(int index)
    {
        levelToLoad = index;
        fader.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
        
    }
}
