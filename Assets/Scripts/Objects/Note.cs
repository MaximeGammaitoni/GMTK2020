using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 0.7f;
    public float timer = 0;
    private float Lifetime = 5;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer> Lifetime)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
        transform.position += new Vector3(speed * -Time.deltaTime, 0, 0);
    }
}
