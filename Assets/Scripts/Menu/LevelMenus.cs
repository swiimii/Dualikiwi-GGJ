using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenus : MonoBehaviour
{
    public static LevelMenus Singleton;
    public GameObject deathMenu, victoryMenu;
    bool isShowingMenu = false;

    private void Start()
    {
        Singleton = this;
    }

    public void ShowDeathMenu()
    {
        if (!isShowingMenu)
        {
            isShowingMenu = true;
            deathMenu.SetActive(true);
        }
    }

    public void ShowVictoryMenu()
    {
        if (!isShowingMenu)
        {
            isShowingMenu = true;
            victoryMenu.SetActive(true);
        }
    }
}
