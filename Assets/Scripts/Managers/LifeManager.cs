using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager
{
    public GameObject Life1Display;
    public GameObject Life2Display;
    public GameObject Life3Display;

    public bool life1 = true;
    public bool life2 = true;
    public bool life3 = true;

    public bool outOfControlMode = false;
    // Start is called before the first frame update
    public LifeManager()
    {
        Life1Display = GameObject.Find("LIFE1");
        Life2Display = GameObject.Find("LIFE2");
        Life3Display = GameObject.Find("LIFE3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LooseLife()
    {
        if (life1)
        {
            life1 = !life1;
            Life1Display.GetComponent<Text>().color = Color.Lerp(Life1Display.GetComponent<Text>().color, Color.red, 1f);
            Life1Display.GetComponentInChildren<Image>().color = Color.Lerp(Life1Display.GetComponentInChildren<Image>().color, new Color(.2f, .2f, .2f, .5f), 1f);
        }
        else if (life2)
        {
            life2 = !life2;
            Life2Display.GetComponent<Text>().color = Color.Lerp(Life2Display.GetComponent<Text>().color, Color.red, 1f);
            Life2Display.GetComponentInChildren<Image>().color = Color.Lerp(Life2Display.GetComponentInChildren<Image>().color, new Color(.2f, .2f, .2f, .5f), 1f);
        }  
        else if (life3)
        {
            life3 = !life3;
            Life3Display.GetComponent<Text>().color = Color.Lerp(Life3Display.GetComponent<Text>().color, Color.red, 1f);
            Life3Display.GetComponentInChildren<Image>().color = Color.Lerp(Life3Display.GetComponentInChildren<Image>().color, new Color(.2f, .2f, .2f, .5f), 1f);
            outOfControlMode = true;
            GameManager.singleton.ScoreManager.StartDecrease();
        }     
    }

    public void GainLife()
    {
        if (!life3)
        {
            life3 = true;
            Life3Display.GetComponent<Text>().color = Color.Lerp(Life3Display.GetComponent<Text>().color, Color.grey, 1f);
            Life3Display.GetComponentInChildren<Image>().color = Color.Lerp(Life3Display.GetComponentInChildren<Image>().color, Color.white, 1f);
        }
            
    }
}
