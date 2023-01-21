using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class ObjectiveTrigger : MonoBehaviour
{
   
    public TriggerType triggerType;
    public InteractionType interactionType;
    public Objective newObjective;
    public int completeId;
    [TextArea]
    public string animText;
    public float animTime;

    bool isObjComplete = false;
    bool isObjGiven = false;
    
    public PlayableAsset timeLineAsset;
    
    public GameObject anyRequiredUI;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            if(anyRequiredUI!=null)
                anyRequiredUI.SetActive(true);
            if (interactionType == InteractionType.clickToInteract)
            {
                return;   
            }
            else
            {
                if (triggerType == TriggerType.NewObjective)
                {
                    if (!isObjGiven)
                    {
                        SetNewObjective();
                        playCutscene();
                        isObjGiven = true;
                    }

                }

                else if (triggerType == TriggerType.CompleteObjective)
                {

                    if (!isObjComplete)
                    {
                        CompleteObjective(completeId);
                        playCutscene();
                        isObjComplete = true;
                    }

                }

                else
                {
                    CompleteAndSetNewObjective(completeId);
                    playCutscene();
                    
                }
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (anyRequiredUI != null)
                anyRequiredUI.SetActive(false);
            if(interactionType==InteractionType.walkToInteract)
            {
                this.gameObject.SetActive(false);
            }
        
        }
    }
    private void OnTriggerStay(Collider other)
    {
     if(other.gameObject.CompareTag("Player"))
        {
            if (interactionType == InteractionType.clickToInteract)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (triggerType == TriggerType.NewObjective)
                    {
                        if(!isObjGiven)
                        {
                            SetNewObjective();
                            playCutscene();
                            isObjGiven = true;
                            this.gameObject.SetActive(false);
                        }

                    }

                    else if (triggerType == TriggerType.CompleteObjective)
                    {

                        if(!isObjComplete)
                        {
                            CompleteObjective(completeId);
                            playCutscene();
                            isObjComplete = true;
                            this.gameObject.SetActive(false);
                        }
                        
                        
                    }

                    else
                    {
                        CompleteAndSetNewObjective(completeId);
                        playCutscene();
                        this.gameObject.SetActive(false);
                    }
                }

            }
        }
    }

    void playCutscene()
    {
        if(timeLineAsset!=null )
            Interior2Timeline.instance.Play(timeLineAsset);
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
                if (anyRequiredUI != null)
                    anyRequiredUI.SetActive(false);
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

public enum InteractionType
{
    clickToInteract,
    walkToInteract
}
public enum TriggerType
{
    NewObjective,
    CompleteObjective,
    CompelteAndGiveNewObjective
}

 
