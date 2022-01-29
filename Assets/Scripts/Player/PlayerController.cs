using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");

        // Moving Horizontally, not both
        if (Mathf.Abs(horizontalMovement) > .1f && !(Mathf.Abs(verticalMovement) > .1f))
        {
            transform.position += (Vector3)Vector2.right * horizontalMovement * .1f;
        }

        // Moving Vertically, not both
        if (Mathf.Abs(verticalMovement) > .1f && !(Mathf.Abs(horizontalMovement) > .1f))
        {
            transform.position += (Vector3)Vector2.up * verticalMovement * .1f;
        }
    }
}
