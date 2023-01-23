using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_behavior : MonoBehaviour
{
    public Image[] arr=new Image[4];
    public Transform[] tr=new Transform[4];
    private int current=0;
    public bool completed=false;
    // Start is called before the first frame update
    void Start()
    {
        selected();
    }

    // Update is called once per frame
    void Update()
    {
      if(!completed)
      {
        if(Input.GetKeyDown("w"))
        {
          if(current>0 && current<=3)
          {
            deselect();
            current=current-1;
            selected();
          }
        }
        if(Input.GetKeyDown("s"))
        {
          if(current>=0 && current<3)
          {
            deselect();
            current=current+1;
            selected();
          }
        }
        if(Input.GetKeyDown("a"))
        {
            rotate_left();
        }
        if(Input.GetKeyDown("d"))
        {
            rotate_right();
        }
        float a=0;
        for(int i=0;i<3;i++)
        {
          if(tr[i].rotation.eulerAngles.z>=a)
          {
            a=Mathf.Floor(tr[i].rotation.eulerAngles.z);
            Debug.Log(a);
          }
        }
        if(a==0)
        {
          completed=!completed;
          deselect();
        }
      }

    }

    private void selected()
    {
        arr[current].color= new Color32(132,127,188,255);
    }
    private void deselect()
    {
        arr[current].color= new Color32(255,255,255,255);
    }
    private void rotate_left()
    {
        tr[current].Rotate(0,0,-30);
        //arr[current].getComponent<transform>.Rotation.z=arr[current].Rotation.z-30;
    }
    private void rotate_right()
    {
        tr[current].Rotate(0,0,30);
        //arr[current].Rotation.z=arr[current].Rotation.z+30;
    }
}
