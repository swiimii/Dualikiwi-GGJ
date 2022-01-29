using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool isTriggered = false;
    public abstract void DoInteract(Vector3 direction);
}
