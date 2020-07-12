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
        notes.Remove(notes[0]);
        if (notes.Count == 0)
            noteIsIn = false;
        other.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.blue;
        GameManager.singleton.ScoreManager.ComboCounter();
        GameManager.singleton.LifeManager.LooseLife();
    }
    private void Update()
    {
        if (Input.anyKeyDown && noteIsIn)
        {
            string keyPressed = Input.inputString;

            if (Input.inputString.ToUpper() == GameManager.singleton.TextPickerManager.letterDestroyed.text)
            {
                if (notes.Count > 0)
                {
                    noteIsIn = false;
                }
                GameObject circle = Instantiate(GameManager.singleton.LevelBuilderManager.CirclePrefab, new Vector3(0,0,1),Quaternion.identity);
                notes[notes.Count - 1].gameObject.GetComponent<Note>().timer = 0;
                //GameManager.singleton.TextPickerManager.ColorizeCharGreen();
                GameManager.singleton.TextPickerManager.GetNextChar();
                notes[notes.Count - 1].gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.blue;
                notes[notes.Count - 1].SetActive(false);
                notes.Remove(notes[notes.Count - 1]);
                GameManager.singleton.ScoreManager.isStreaking = true;
                GameManager.singleton.ScoreManager.ComboCounter();
                GameManager.singleton.ScoreManager.IncrementScore();
                GameManager.singleton.ScoreManager.MultiplieScore();
                if (GameManager.singleton.LifeManager.outOfControlMode)
                {
                    GameManager.singleton.LifeManager.outOfControlMode = false;
                    GameManager.singleton.LifeManager.GainLife();
                }
                    

            }
            else
            {
                CameraController.instance.ScreenShake(0.2f, 0.15f);
                GameManager.singleton.ScoreManager.isStreaking = false;
                GameManager.singleton.ScoreManager.ComboCounter();
                GameManager.singleton.LifeManager.LooseLife();
            }
        }
    }
}
