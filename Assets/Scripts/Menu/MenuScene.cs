using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    public static bool alreadyLoadedCredits = false;
    public GameObject title, rightSword, leftSword;
    public GameObject[] interfaceItems;
    public Text[] creditsTextObjects;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainMenuAnimation());
    }

    public IEnumerator MainMenuAnimation()
    {
        // menu should pop in after 16 seconds
        if (!alreadyLoadedCredits)
        {
            alreadyLoadedCredits = true;
            var introDuration = 16f;
            StartCoroutine(ShowOpeningCredits(introDuration - 1f));

            yield return new WaitForSeconds(introDuration - 1f);
        }

        var rightSwordTarget = new Vector3(title.transform.position.x, rightSword.transform.position.y, rightSword.transform.position.z);
        var leftSwordTarget = new Vector3(title.transform.position.x, leftSword.transform.position.y, leftSword.transform.position.z);

        StartCoroutine(MoveToPosition(rightSword, rightSwordTarget, 1f));
        StartCoroutine(MoveToPosition(leftSword, leftSwordTarget, 1f));

        yield return new WaitForSeconds(1f);

        title.SetActive(true);
        foreach(var item in interfaceItems)
        {
            item.SetActive(true);
        }
        
    }
    
    public IEnumerator MoveToPosition(GameObject obj, Vector3 target, float duration)
    {
        var origin = obj.transform.position;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            yield return null;
            elapsed += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(origin, target, elapsed / duration);
        }
    }

    public IEnumerator ShowOpeningCredits(float totalDuration)
    {
        var numItems = creditsTextObjects.Length;
        var durationPerItem = totalDuration / numItems;

        foreach (var item in creditsTextObjects)
        {
            item.gameObject.SetActive(true);
            item.color = new Color(item.color.r, item.color.g, item.color.b, 0);
            var elapsed = 0f;
            while (elapsed < durationPerItem)
            {
                yield return null;
                elapsed += Time.deltaTime;
                item.color = new Color(item.color.r, item.color.g, item.color.b, Mathf.Sin(elapsed / durationPerItem * Mathf.PI));
            }
            item.gameObject.SetActive(false);
        }
    }



}
