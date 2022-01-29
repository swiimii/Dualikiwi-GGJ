using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonation : MonoBehaviour
{
    public float duration = .5f;
    void Start()
    {
        StartCoroutine(Dissipate());
    }

    public IEnumerator Dissipate()
    {
        float elapsed = 0f;
        var originalScale = transform.localScale;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = new Vector3(Mathf.Lerp(originalScale.x, 0, elapsed / duration),
                                               Mathf.Lerp(originalScale.y, 0, elapsed / duration),
                                               originalScale.z);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            if (collision.gameObject.TryGetComponent<Interactable>(out var interactable) && !interactable.isTriggered)
            {
                interactable.DoInteract(interactable.transform.position - transform.position);
            }
            if (collision.gameObject.TryGetComponent<KiwiController>(out var kiwiController))
            {
                kiwiController.DefeatCharacter();
            }
        }
    }

}
