using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if (Input.anyKeyDown)
        {        
            string keyPressed = Input.inputString;

            if (Input.inputString == GameManager.singleton.TextPickerManager.letterDestroyed.text)
            {
                other.gameObject.GetComponent<Note>().timer = 0;
                //GameManager.singleton.TextPickerManager.ColorizeCharGreen();
                GameManager.singleton.TextPickerManager.GetNextChar();
                other.gameObject.SetActive(false);
            }
            else
            {
                //life -- 
                CameraController.instance.ScreenShake(0.2f, 0.15f);
            }
        }
    }
}
