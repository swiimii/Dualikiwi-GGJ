using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : KiwiController
{
    private void FixedUpdate()
    {
        if (!isPerformingAction)
        {
            // Moving Horizontally, not both
            var direction = Vector3.zero;
            var receivedValidInput = false;
            if (Mathf.Abs(horizontalMovement) > .1f && !(Mathf.Abs(verticalMovement) > .1f))
            {
                // transform.position += (Vector3)Vector2.right * horizontalMovement * .1f;
                direction = horizontalMovement * Vector3.right;
                receivedValidInput = true;
            }

            // Moving Vertically, not both
            if (Mathf.Abs(verticalMovement) > .1f && !(Mathf.Abs(horizontalMovement) > .1f))
            {
                // transform.position += (Vector3)Vector2.up * verticalMovement * .1f;
                direction = verticalMovement * Vector3.up;
                receivedValidInput = true;
            }

            if (receivedValidInput)
            {
                TryMoveOrInteract(direction);
            }
        }
    }
}
