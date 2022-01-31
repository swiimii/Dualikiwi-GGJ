using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenus : MonoBehaviour
{
    public static LevelMenus Singleton;
    public GameObject deathMenu, victoryMenu;
    bool isShowingMenu = false;

    private void Start()
    {
        Singleton = this;
    }

    public IEnumerator ShowDeathMenu()
    {
        yield return new WaitForSeconds(1);
        if (!isShowingMenu)
        {
            isShowingMenu = true;
            deathMenu.SetActive(true);
        }
    }

    public IEnumerator ShowVictoryMenu()
    {
        yield return new WaitForSeconds(1);
        var isPlayerDefeated = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsDefeated;
        if (!isShowingMenu && !isPlayerDefeated)
        {
            isShowingMenu = true;
            victoryMenu.SetActive(true);

            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            int maxLevelsComplete = PlayerPrefs.HasKey("LevelsCompleted") ? PlayerPrefs.GetInt("LevelsCompleted") : 0;
            PlayerPrefs.SetInt("LevelsCompleted", Mathf.Max(sceneIndex, maxLevelsComplete)); // Main menu is index 0, level 1 is index 1, etc... 
            PlayerPrefs.Save();
        }
    }

    public void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
    }
}
