using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : KiwiController
{
    private void FixedUpdate()
    {
        if (!isPerformingAction && !IsDefeated)
        {
            // Moving Horizontally, not both
            var direction = Vector3.zero;
            var receivedValidInput = false;
            if (Mathf.Abs(horizontalMovement) > .1f && !(Mathf.Abs(verticalMovement) > .1f))
            {
                direction = horizontalMovement * Vector3.right;
                receivedValidInput = true;
            }

            // Moving Vertically, not both
            if (Mathf.Abs(verticalMovement) > .1f && !(Mathf.Abs(horizontalMovement) > .1f))
            {
                direction = verticalMovement * Vector3.up;
                receivedValidInput = true;
            }

            if (receivedValidInput && Mathf.Abs(direction.x) > .1)
            {
                GetComponent<SpriteRenderer>().flipX = direction.x > 0;
            }

            var animator = GetComponent<Animator>();
            animator.SetFloat("HorizontalMovement", direction.x);
            animator.SetFloat("VerticalMovement", direction.y);
            animator.SetBool("IsInteracting", false); // Set this in TryMoveOrInteract()
            if (receivedValidInput)
            {
                TryMoveOrInteract(direction);
            }
        }
    }

    public override void DeathEffect()
    {
        
        GetComponent<AudioSource>().Play();
        StartCoroutine(LevelMenus.Singleton.ShowDeathMenu());
    }

    public override void CollideAction()
    {
        DefeatCharacter();
    }
}
