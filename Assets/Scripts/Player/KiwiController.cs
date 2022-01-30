using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterState))]
public abstract class KiwiController : MonoBehaviour
{
    protected float horizontalMovement = 0;
    protected float verticalMovement = 0;

    protected bool isPerformingAction = false;

    protected float actionDuration = .3f;

    public bool IsDefeated = false;

    // Update is called once per frame
    protected virtual void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        horizontalMovement = Input.acceleration.x / 2;
        verticalMovement = Input.acceleration.y / 2;
#else
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
#endif
    }

    public void TryMoveOrInteract(Vector3 direction)
    {

        if (!TryRaycast<Transform>(direction, out var tf))
        {
            // If raycast doesn't hit anything, move one space.
            StartCoroutine(MoveOneSpace(direction));
        }
        else
        {
            if (TryRaycast<Interactable>(direction, out var interactable) && !interactable.isTriggered)
            {
                interactable.DoInteract(direction);
                StartCoroutine(WaitForInteract());
                print("Interaction!");
            }
            if (TryRaycast<Hazard>(direction, out var hazard))
            {
                if (hazard.isDangerActive)
                {
                    // move then die
                    StartCoroutine(MoveOneSpace(direction, true));
                }
            }
            if (TryRaycast<KiwiController>(direction, out var kiwiController))
            {
                CollideAction();
            }
        }
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

    public IEnumerator WaitForInteract()
    {
        var animator = GetComponent<Animator>();
        isPerformingAction = true;
        animator.SetBool("IsInteracting", true);
        yield return new WaitForSeconds(actionDuration);
        animator.SetBool("IsInteracting", false);
        isPerformingAction = false;
    }

    public IEnumerator MoveOneSpace(Vector3 direction, bool defeatAfterMoving = false)
    {
        isPerformingAction = true;
        var progress = 0f;
        
        var origin = transform.position;
        var targetLocation = transform.position + direction.normalized;

        while (progress < actionDuration)
        {
            yield return null;
            progress += Time.deltaTime;
            transform.position = Vector3.Lerp(origin, targetLocation, progress / actionDuration);
        }

        if (defeatAfterMoving || CheckIfCollidingWithEnemy())
        {
            GetComponent<KiwiController>().DefeatCharacter();
        }

        isPerformingAction = false;
    }

    public bool CheckIfCollidingWithEnemy()
    {
        var col = GetComponent<BoxCollider2D>();
        var enemyCol = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BoxCollider2D>();
        return col.IsTouching(enemyCol);
    }

    public void DefeatCharacter()
    {
        IsDefeated = true;
        GetComponent<Animator>().SetBool("IsDead", true);
        DeathEffect();
        // do other things.
    }

    public abstract void CollideAction();

    public abstract void DeathEffect();

}
