using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool IsDefeated = false;

    public void DefeatPlayer()
    {
        IsDefeated = true;
        print("ded");
        // do other things.
    }
}
