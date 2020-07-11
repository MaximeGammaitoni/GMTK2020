using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    
    private bool noteIsIn;
    private GameObject currentNote;
    private List<GameObject> notes;

    private void Start()
    {
        notes = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.green;
        currentNote = other.gameObject;
        notes.Add(currentNote);
        noteIsIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        notes.Remove(currentNote);
        noteIsIn = false;
        other.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.blue;
    }
    private void Update()
    {
        if (Input.anyKeyDown && noteIsIn)
        {
            string keyPressed = Input.inputString;

            if (Input.inputString == GameManager.singleton.TextPickerManager.letterDestroyed.text)
            {
                GameObject circle = Instantiate(GameManager.singleton.LevelBuilderManager.CirclePrefab, new Vector3(0,0,1),Quaternion.identity);
                currentNote.gameObject.GetComponent<Note>().timer = 0;
                //GameManager.singleton.TextPickerManager.ColorizeCharGreen();
                GameManager.singleton.TextPickerManager.GetNextChar();
                currentNote.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.blue;
                currentNote.gameObject.SetActive(false);
            }
            else
            {
                //life -- 
                CameraController.instance.ScreenShake(0.2f, 0.15f);
            }
        }
    }
}
