using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterCatcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnGUI()
    {

        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            Debug.Log("Expected key: " + GameManager.singleton.TextPickerManager.currentChar);
            if (e.keyCode.ToString().ToLower() == GameManager.singleton.TextPickerManager.currentChar)
            {
                //GameManager.singleton.TextPickerManager.ColorizeCharGreen();
                GameManager.singleton.TextPickerManager.GetNextChar();
                //GameManager.singleton.TextPickerManager.RemoveChar();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
