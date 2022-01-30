using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public GameObject[] canvases;

    public void ToggleCanvas(int index)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (i == index)
            {
                canvases[i].SetActive(true);
            }
            else
            {
                canvases[i].SetActive(false);
            }
        }
    }
}
