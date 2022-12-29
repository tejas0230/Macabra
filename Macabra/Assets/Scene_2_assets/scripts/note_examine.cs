using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_examine : MonoBehaviour
{
  [SerializeField]
  private bool state_check=false;
  [SerializeField]
  private GameObject current_object;
    // Start is called before the first frame update
    void Start()
    {
        state_check=current_object.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown("space"))
      {
        interaction();
        Debug.Log("here");
      }

    }

    public void interaction()
    {
      if(!state_check)
      {
        current_object.SetActive(true);
      }
      else
      {
        current_object.SetActive(false);
      }
    }
}
