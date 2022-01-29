using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Hazard))]
public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Hazard>().isDangerActive = true;
    }
}
