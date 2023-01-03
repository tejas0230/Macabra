using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Item/Create New Item")]
public class InventoryItem : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
}
