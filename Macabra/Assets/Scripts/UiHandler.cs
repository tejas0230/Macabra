using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UiHandler : MonoBehaviour
{
    public static UiHandler instance;
    public GameObject MainMenu;
    public bool instructions;
    private Transform a;

    public TMP_Text ObjectiveAccquiredText;
    public Animator ObjectiveTextAnimator;
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
        a=gameObject.transform.GetChild(1);
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

    public void showinstructions()
    {
        MainMenu.SetActive(false);
        instructions=true;
        a.gameObject.SetActive(true);
    }

    public void backtomainmenu()
    {
      MainMenu.SetActive(true);
      instructions=false;
      a.gameObject.SetActive(false);
    }

    public void endscreen()
    {
        //SceneController.instance.FadeToLevel(3);// this is for fading to the credits scene
        Application.Quit();

    }


    public void StartObjectiveAnim(string text, float timeToWait)
    {
        ObjectiveAccquiredText.text = text;
        ObjectiveTextAnimator.SetBool("ObjectiveAccquired",true);
        StartCoroutine(resetAnimatorBool(timeToWait));
    }

    IEnumerator resetAnimatorBool(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        ObjectiveTextAnimator.SetBool("ObjectiveAccquired", false);
    }
}
