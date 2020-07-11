﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            other.gameObject.GetComponent<Note>().timer = 0;
            other.gameObject.SetActive(false);
        }
    }
}
