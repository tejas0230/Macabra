using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleScript : MonoBehaviour,IInteractable
{
    public float MaxRange => 1;
    public GameObject crosshair;
    public GameObject hand;
    public GameObject key;

    public GameObject puzzleCanvas;
    public Puzzle_behavior puzzle;
    public FPSController controller;
    public void OnEndHover()
    {
        hand.SetActive(false);
        crosshair.SetActive(true);
    }

    public void OnInteract()
    {
        controller.CanMove = false;
        controller.CanRotateCam = false;
        puzzleCanvas.SetActive(true);
    }

    public void OnStartHover()
    {
        crosshair.SetActive(false);
        hand.SetActive(true);
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(puzzle.completed)
        {
            controller.CanMove = true;
            controller.CanRotateCam = true;
            key.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
