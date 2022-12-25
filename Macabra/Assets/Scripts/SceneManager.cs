using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
   
    public Animator fader;
    private int levelToLoad;
    public GameObject mainMenu;
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
        if(levelToLoad!=0)
        {
            mainMenu.SetActive(false);
        }
    }
}
