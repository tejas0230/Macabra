using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUIManager : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject objectiveText;
    public GameObject parent;
    GameObject toBeDeleted;
    int onGoingsize;
    int completedSize;
    bool isUIUpdated = false;

    List<GameObject> uiList = new List<GameObject>();
    private void Start()
    {
        onGoingsize = ObjectiveManager.instance.OnGoingObjective.Count;
        completedSize = ObjectiveManager.instance.CompletedObjectives.Count;
    }
    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Tab))
        {
            InventoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            InventoryPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        if(ObjectiveManager.instance.OnGoingObjective.Count>onGoingsize && !isUIUpdated)
        {
            isUIUpdated = true;
            onGoingsize++;
            GameObject ui = Instantiate(objectiveText);
            uiList.Add(ui); 
            ui.transform.SetParent(parent.transform);
            ui.GetComponent<TMP_Text>().text =  ObjectiveManager.instance.OnGoingObjective[onGoingsize - 1].title;
            ui.GetComponent<TMP_Text>().text.TrimStart();
            StartCoroutine(resetUIBool());
        }

        if (ObjectiveManager.instance.CompletedObjectives.Count > completedSize && !isUIUpdated)
        {
            isUIUpdated = true;
            completedSize++;
            onGoingsize--;
            foreach(GameObject g in uiList)
            {
                if(g.GetComponent<TMP_Text>().text == ObjectiveManager.instance.CompletedObjectives[completedSize-1].title)
                {
                    uiList.Remove(g);
                    Destroy(g);
                    break;
                    
                }
            }
            StartCoroutine(resetUIBool());
        }
    }
    


    IEnumerator resetUIBool()
    {
        yield return new WaitForSeconds(1);
        isUIUpdated = false;
    }
}
