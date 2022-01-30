using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<BackgroundEffect>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        
        StartCoroutine(DoBackgroundEffect());
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator DoBackgroundEffect()
    {
        var elapsed = 0f;
        while (true)
        {
            yield return null;
            elapsed += Time.deltaTime;
            var colorVal = (Mathf.Sin(elapsed) + 1f) * .2f + .1f;
            // print(colorVal);
            Camera.main.backgroundColor = new Color(colorVal, colorVal, colorVal);
        }
    }
}
