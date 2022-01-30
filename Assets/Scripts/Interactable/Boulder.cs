using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Interactable
{

    public bool isHittingObject = false;
    public bool isDangerous = false;
    public bool canTriggerOthers = false;
    public AudioClip rockSmash;

    bool isBeingDestroyed = true;

    public override void DoInteract(Vector3 direction)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            GetComponent<AudioSource>().Play();
            StartCoroutine(BoulderRoll(direction));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTriggered && !collision.gameObject.GetComponent<Detonation>())
        {
            isHittingObject = true;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(rockSmash);
            print("Boulder hit");

            if (collision.gameObject.TryGetComponent<Spikes>(out var spikes))
            {
                isDangerous = false;
                isBeingDestroyed = false;
                print("Hit spikes!");
                spikes.isDangerActive = false;
                StartCoroutine(FallIntoPit(spikes.transform));
                GetComponent<Collider2D>().enabled = false;
                spikes.GetComponent<Collider2D>().enabled = false;
            }
            else if (collision.gameObject.TryGetComponent<KiwiController>(out var kiwiController))
            {
                kiwiController.DefeatCharacter();
            }
            else if (canTriggerOthers && collision.gameObject.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.DoInteract(interactable.transform.position - transform.position);
            }
        }
    }

    public IEnumerator BoulderRoll(Vector3 direction)
    {
        // start roll. become lethal on contact after .5s
        // on hitting object, stop lethal, then destroy self.

        // if a pit is collided with, this coroutine will be
        // interrupted
        isDangerous = true;
        var speed = 3f;
        while (!isHittingObject)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1);
        if (isBeingDestroyed)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FallIntoPit(Transform pitLocation)
    {
        var moveDuration = .28f;
        var progress = 0f;
        var origin = transform.position;

        while (progress < moveDuration)
        {
            progress += Time.deltaTime;
            transform.position = Vector2.Lerp(origin, pitLocation.position, progress / moveDuration);
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.grey;
        transform.position += new Vector3(0, 0, .01f);
    }
}
