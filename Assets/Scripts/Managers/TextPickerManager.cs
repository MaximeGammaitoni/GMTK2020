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

    private int currentCharIndex = 0;
    public string currentChar;
    private string nextChar;

    //public GameObject inputField;
    //public GameObject textDisplay;

    public GameObject nextWordDisplay;
    public GameObject wordDisplay;
    public GameObject letterDestroyer;

    public TextPickerManager()
    {

        WordList.Add("lorem");
        WordList.Add("ipsum");
        WordList.Add("dolor");
        WordList.Add("sit");
        WordList.Add("amet");




        currentWordLength = WordList[currentWordIndex].Length;
        
        currentChar = WordList[currentWordIndex].Substring(currentCharIndex, 1);



        wordDisplay = GameObject.Find("WordDisplay");
        nextWordDisplay = GameObject.Find("NextWordDisplay");
        letterDestroyer = GameObject.Find("LetterDestroyer");

        currentWord = wordDisplay.GetComponent<Text>();
        nextWord = nextWordDisplay.GetComponent<Text>();
        
        currentWord.text = WordList[currentWordIndex] + " <==";
        nextWord.text = WordList[currentWordIndex + 1] + " <-";

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
            /*Debug.Log("currentLetterIndex: " + currentCharIndex);
            Debug.Log("CurrentWordLength: " + WordList[currentWordIndex].Length);*/
            RemoveChar();
            currentCharIndex++;
            currentChar = WordList[currentWordIndex].Substring(currentCharIndex, 1);
            
        }
        else if(currentCharIndex == WordList[currentWordIndex].Length - 1)
        {
            RemoveChar();
            ChangeWord();
        }

       
/*        Debug.Log(currentChar);
        Debug.Log(currentCharIndex);*/
    }

    public void ColorizeCharGreen()
    {
        currentWord.text = currentWord.text.Replace(currentWord.text[currentCharIndex].ToString(), "<color=#32a852>" + currentWord.text[currentCharIndex].ToString() + "</color>");

    }

    public void FragmentWord()
    {
        //currentWord.text = currentWord.text.Substring(WordList[currentWordIndex]
    }

    public void RemoveChar()
    {
        //letterDestroyer. currentWord.text
        currentWord.text= currentWord.text.Remove(0, 1);

    }


    public void ChangeWord()
    {
        if(currentWordIndex < WordList.Count)
        {
            currentCharIndex = 0;
            currentWordIndex++;
            currentChar = WordList[currentWordIndex].Substring(currentCharIndex, 1);
            currentWord.text = WordList[currentWordIndex] + " <==";
            nextWord.text = WordList[currentWordIndex + 1] + "<-";
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
