using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalMovement = 0;
    float verticalMovement = 0;

    bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!isMoving)
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
                if (CheckDirection(direction))
                {
                    StartCoroutine(MoveOneSpace(direction));
                }
                else
                {
                    if (TryRaycast<Interactable>(direction, out var icomp))
                    {
                        icomp.DoInteract(direction);
                        print("Interaction!");
                    }
                    if (TryRaycast<Hazard>(direction, out var hcomp))
                    {
                        if (hcomp.isDangerActive)
                        {
                            StartCoroutine(MoveOneSpace(direction));
                        }
                    }
                }
            }
        }
    }

    public bool CheckDirection(Vector3 direction)
    {
        var hit = Physics2D.Raycast(transform.position, direction.normalized, 1f);
        if (hit && hit.collider)
        {
            print(hit.collider.gameObject);
            return false;
        }
        return true;
    }

    public bool TryRaycast<T>(Vector3 direction, out T comp)
    {
        var hit = Physics2D.Raycast(transform.position, direction.normalized, 1f);
        if (hit && hit.collider && hit.collider.gameObject && hit.collider.gameObject.TryGetComponent<T>(out var outComp))
        {
            comp = outComp;
            return true;
        }
        comp = default(T);
        return false;
    }

    public IEnumerator MoveOneSpace(Vector3 direction, bool defeatAfterMoving=false)
    {

        isMoving = true;
        var progress = 0f;
        var duration = .25f;
        var origin = transform.position;
        var targetLocation = transform.position + direction.normalized;

        while (progress < duration)
        {
            yield return null;
            progress += Time.deltaTime;
            transform.position = Vector3.Lerp(origin, targetLocation, progress/duration);
        }
        
        isMoving = false;

        if(defeatAfterMoving)
        {
            GetComponent<PlayerState>().DefeatPlayer();
        }
    }
}
