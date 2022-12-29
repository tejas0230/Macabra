using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    public static UiHandler instance;
    public GameObject MainMenu;
    
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
      if(SceneManager.GetActiveScene().buildIndex==1)
        {
            MainMenu.SetActive(false);
        }
    }

    public void startNewGame()
    {
        SceneController.instance.FadeToLevel(1);
        //MainMenu.SetActive(false);
    }
}
