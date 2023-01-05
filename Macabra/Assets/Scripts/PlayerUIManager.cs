using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerUIManager : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject objectiveText;
    public GameObject parent;
    GameObject toBeDeleted;
    int onGoingsize;
    int completedSize;
    bool isUIUpdated = false;
    bool isInventoryUpdated = false;
    List<GameObject> uiList = new List<GameObject>();


    public Transform inventoryPanel;
    public GameObject itemUI;

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

            if (!isInventoryUpdated)
            {
                isInventoryUpdated = true;
                updateInvetory();
                StartCoroutine(resetInventoryBool());
                
            }
            if (ObjectiveManager.instance.OnGoingObjective.Count > onGoingsize && !isUIUpdated)
            {
                isUIUpdated = true;
                onGoingsize++;
                GameObject ui = Instantiate(objectiveText);
                uiList.Add(ui);
                ui.transform.SetParent(parent.transform);
                ui.GetComponent<TMP_Text>().text = ObjectiveManager.instance.OnGoingObjective[onGoingsize - 1].title;
                ui.GetComponent<TMP_Text>().text.TrimStart();
                StartCoroutine(resetUIBool());
            }

            if (ObjectiveManager.instance.CompletedObjectives.Count > completedSize && !isUIUpdated)
            {
                isUIUpdated = true;
                completedSize++;
                onGoingsize--;
                foreach (GameObject g in uiList)
                {
                    if (g.GetComponent<TMP_Text>().text == ObjectiveManager.instance.CompletedObjectives[completedSize - 1].title)
                    {
                        uiList.Remove(g);
                        Destroy(g);
                        break;

                    }
                }
                StartCoroutine(resetUIBool());
            }

            
        }
        else
        {
            InventoryPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        
    }
    
    void updateInvetory()
    {
        foreach( Transform item in inventoryPanel)
        {
            Destroy(item.gameObject);
        }
        foreach (InventoryItem item in InventoryManager.instance.items)
        {
            GameObject obj = Instantiate(itemUI, inventoryPanel);
            Image objImg = obj.transform.Find("Icon").GetComponent<Image>();
            TMP_Text objText = obj.transform.Find("Name").GetComponent<TMP_Text>();
            objImg.sprite = item.icon;
            objText.text = item.itemName;
        }
    }

    IEnumerator resetUIBool()
    {
        yield return new WaitForSeconds(0.5f);
        isUIUpdated = false;
    }
    IEnumerator resetInventoryBool()
    {
        yield return new WaitForSeconds(0.5f);
        isInventoryUpdated = false;
    }
}
