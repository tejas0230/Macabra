using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Objective New Item")]
public class ObjectiveScriptableObject : ScriptableObject
{
    public List<Objective> AllObjectives= new List<Objective>();
}
