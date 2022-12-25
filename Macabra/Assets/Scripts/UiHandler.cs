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
      
    }
}
