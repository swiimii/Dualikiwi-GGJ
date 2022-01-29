using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Interactable
{

    public bool isTriggered = false;
    public bool isHittingObject = false;

    public override void DoInteract(Vector3 direction)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            StartCoroutine(BoulderRoll(direction));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTriggered)
        {
            isHittingObject = true;
            print("Boulder hit");
        }
    }

    public IEnumerator BoulderRoll(Vector3 direction)
    {
        // start roll. become lethal on contact after .5s
        // on hitting object, stop lethal, then destroy self.
        var speed = 3f;
        while (!isHittingObject)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
