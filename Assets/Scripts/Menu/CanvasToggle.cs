using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public GameObject canvas1, canvas2;

    public void ToggleCanvas()
    {
        if (canvas1.activeInHierarchy)
        {
            canvas2.SetActive(true);
            canvas1.SetActive(false);
        }
        else
        {
            canvas1.SetActive(true);
            canvas2.SetActive(false);
        }
    }
}
