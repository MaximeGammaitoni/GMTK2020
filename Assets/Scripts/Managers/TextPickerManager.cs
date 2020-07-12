using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.UI;
using UnityEngine;

public class TextPickerManager
{

    public List<string> WordList = new List<string>();
    private int currentWordIndex = 0;
    private Text currentWord;
    private int currentWordLength;
    private Text nextWord;
    public Text letterDestroyed;

    private int currentCharIndex = 0;
    public string currentChar;
    private string nextChar;
    public GameObject destroyerPrefab;

    //public GameObject inputField;
    //public GameObject textDisplay;

    public GameObject nextWordDisplay;
    public GameObject wordDisplay;
    public GameObject letterDestroyer;

    public GameObject wordPanel;

    public RectTransform destroyerOriginTransform;

    public TextPickerManager()
    {

        WordList.Add("lorem");
        WordList.Add("ipsum");
        WordList.Add("dolor");
        WordList.Add("sit");
        WordList.Add("amet");

        currentWordLength = WordList[currentWordIndex].Length;
        
        currentChar = WordList[currentWordIndex].Substring(currentCharIndex, 1);

        destroyerPrefab = Resources.Load<GameObject>("LetterDestroyer");
        wordDisplay = GameObject.Find("WordDisplay");
        nextWordDisplay = GameObject.Find("NextWordDisplay");
        letterDestroyer = GameObject.Find("LetterDestroyer");

        wordPanel = GameObject.Find("WordPanel");
        destroyerOriginTransform = GameObject.Find("destroyerTransform").GetComponent<RectTransform>();

        currentWord = wordDisplay.GetComponent<Text>();
        nextWord = nextWordDisplay.GetComponent<Text>();
        letterDestroyed = letterDestroyer.GetComponent<Text>();
        
        currentWord.text = WordList[currentWordIndex];
        nextWord.text = WordList[currentWordIndex + 1];

        FragmentWord();

        //inputField = GameObject.Find("InputField");
        //textDisplay = GameObject.Find("TextDisplay");
        //GameManager.GameUpdate += Update;
        //string playerInput = textDisplay.GetComponent<Text>().text;
        //playerInput = ">  " + playerInput + "  <";
        //GameManager.singleton.TextPickerManager.inputStore();
    }

    public void GetNextChar()
    {
        
        if (currentCharIndex < WordList[currentWordIndex].Length - 1)
        {
            Debug.Log("currentLetterIndex: " + currentCharIndex);
            //Debug.Log("CurrentWordLength: " + WordList[currentWordIndex].Length);
            RemoveChar();
            currentCharIndex++;
            currentChar = WordList[currentWordIndex].Substring(currentCharIndex, 1);
            
        }
        else if(currentCharIndex == WordList[currentWordIndex].Length - 1)//le mot est terminé
        {
            Debug.Log("hat dayum");
            GameManager.singleton.StartCoroutine(ZoomInFadeOut(letterDestroyed?.gameObject));
            //letterDestroyer. currentWord.text
            letterDestroyed.text = letterDestroyed.text.Remove(0, 1);
            ChangeWord();
            
        }

       
/*        Debug.Log(currentChar);
        Debug.Log(currentCharIndex);*/
    }

   /* public void ColorizeCharGreen()
    {
        currentWord.text = currentWord.text.Replace(currentWord.text[currentCharIndex].ToString(), "<color=#32a852>" + currentWord.text[currentCharIndex].ToString() + "</color>");

    }*/

    public void FragmentWord()
    {

        letterDestroyed.text = currentWord.text[0].ToString();
        currentWord.text = currentWord.text.Remove(0, 1);
        //Debug.Log("New Destroyed Letter: "+currentWord.text[0].ToString());
    }

    IEnumerator ZoomInFadeOut(GameObject instance)
    {
        Vector3 target = instance.transform.localScale * 2.0f;
        float ratio = 0;
        float speed = 1.0f;
        Color targetColor = new Color(0, 0, 0, 0);

        while ((instance.transform.localScale != target) || (instance.GetComponent<Text>().color != targetColor))
        {
            instance.transform.localScale = Vector3.Lerp(instance.transform.localScale, target, ratio+=Time.deltaTime/2f * speed);
            instance.transform.GetComponent<Text>().color = Color.Lerp(instance.transform.GetComponent<Text>().color, targetColor, ratio);
            yield return 0;
        }
        GameManager.Destroy(instance);
    }

    public void RemoveChar()
    {
        GameManager.singleton.StartCoroutine(ZoomInFadeOut(letterDestroyed?.gameObject));
        //letterDestroyer. currentWord.text
        letterDestroyed.text = letterDestroyed.text.Remove(0, 1);
        InstantiateNewLetterDestroyer();
        FragmentWord();

    }

    public void InstantiateNewLetterDestroyer()
    {
        GameObject newDestroyedLetter = GameObject.Instantiate(destroyerPrefab, Vector3.zero, Quaternion.identity);
        newDestroyedLetter.GetComponent<RectTransform>().position = destroyerOriginTransform.position;
        Debug.Log("Next letter : "+currentChar);
        newDestroyedLetter.transform.parent = wordPanel.transform;
        newDestroyedLetter.GetComponent<Text>().text = currentChar;
        letterDestroyed = newDestroyedLetter.GetComponent<Text>();
    }

    public void ChangeWord()
    {
        if(currentWordIndex <= WordList.Count-1)
        {
            currentCharIndex = 0;
           
            currentWordIndex++;
            currentChar = WordList[currentWordIndex].Substring(currentCharIndex, 1);
            currentWord.text = WordList[currentWordIndex];
            if(currentWordIndex+1 <= WordList.Count-1)
                nextWord.text = WordList[currentWordIndex + 1];
            else
            {
                nextWord.text = string.Empty;
            }
            InstantiateNewLetterDestroyer();
            FragmentWord();
        }
        
    }
    /*public void inputStore()
    {


    }*/

    /*void Update()
    {
        string playerInput = textDisplay.GetComponent<Text>().text;
        playerInput = ">  " + playerInput + "  <";
    }*/
    // Update is called once per frame

}
