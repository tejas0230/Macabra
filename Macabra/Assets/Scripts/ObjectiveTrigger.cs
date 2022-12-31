using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveTrigger : MonoBehaviour
{
    public TriggerType triggerType;
    public Objective newObjective;
    public int completeId;
    [TextArea]
    public string animText;
    public float animTime;
   // public UnityEvent CompleteObjectiveEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(triggerType == TriggerType.NewObjective)
            {
                SetNewObjective();
            }
            
            else if(triggerType == TriggerType.CompleteObjective)
            {
               
                CompleteObjective(completeId);
            }

            else
            {
                CompleteAndSetNewObjective(completeId);
            }
        }
    }

    public void SetNewObjective()
    {
       if(!ObjectiveManager.instance.OnGoingObjective.Contains(newObjective) && !ObjectiveManager.instance.CompletedObjectives.Contains(newObjective))
        {
            ObjectiveManager.instance.OnGoingObjective.Add(newObjective);
            UiHandler.instance.StartObjectiveAnim(animText, animTime);
        }
        
    }

    public void CompleteObjective(int id)
    {
        foreach(Objective s in ObjectiveManager.instance.OnGoingObjective)
        {
            if(s.objectiveID == id && !ObjectiveManager.instance.CompletedObjectives.Contains(s))
            {
                ObjectiveManager.instance.CompletedObjectives.Add(s);
                ObjectiveManager.instance.OnGoingObjective.Remove(s);
                UiHandler.instance.StartObjectiveAnim(animText, animTime);
                return;
            }
        }
    }

    public void CompleteAndSetNewObjective(int id)
    {
        CompleteObjective(id);
        SetNewObjective();
    }
}

public enum TriggerType
{
    NewObjective,
    CompleteObjective,
    CompelteAndGiveNewObjective
}

 
