using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public bool IsDefeated = false;

    public void DefeatCharacter()
    {
        IsDefeated = true;
        print("ded");
        // do other things.
    }
}
