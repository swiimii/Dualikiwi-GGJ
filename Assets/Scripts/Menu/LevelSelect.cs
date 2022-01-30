using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        var levelsCompleted = PlayerPrefs.HasKey("LevelsCompleted") ? PlayerPrefs.GetInt("LevelsCompleted") : 0;
        for (int i = 0; i < levelButtons.Length && i <= levelsCompleted; i++)
        {
            levelButtons[i].interactable = true;
        }
    }

}
