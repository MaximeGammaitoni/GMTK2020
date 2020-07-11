using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesLayer : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Renderer>().sortingOrder = -10;
    }
}
