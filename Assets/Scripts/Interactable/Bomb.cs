using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bomb : Interactable
{
    public float timer = 3;
    public GameObject detonationPrefab;
    public override void DoInteract(Vector3 direction)
    {
        if (!isTriggered)
        {
            GetComponent<AudioSource>().Play();
            isTriggered = true;
            StartCoroutine(Countdown());
        }
    }

    public IEnumerator Countdown()
    {
        float duration = timer;
        float elapsed = 0f;
        GetComponent<Animator>().SetBool("Detonate", true);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            var timeLeft = (int)(duration - elapsed);
            // update display
            yield return null;
        }
        Detonate();
    }

    public void Detonate()
    {
        var detonation = Instantiate(detonationPrefab);
        detonation.transform.position = transform.position;
        // create explosion object
        Destroy(gameObject);
    }
}
