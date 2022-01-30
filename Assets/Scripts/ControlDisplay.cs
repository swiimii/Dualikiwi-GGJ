using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisplay : MonoBehaviour
{
    public GameObject androidControlsDisplay, pcControlsDisplay;
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        androidControlsDisplay.SetActive(true);
#else
        pcControlsDisplay.SetActive(true);
#endif
    }

}
