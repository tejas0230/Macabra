using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IInteractable
{
    
    float MaxRange { get; }
    void OnStartHover();
    void OnInteract();
    void OnEndHover();
}