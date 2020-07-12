﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuStarter : MonoBehaviour
{
    List<Text> StarterTexts;
    List<string> StarterTextsContents;
    Image TwinGearsLogo;
    GameObject TitleScreenTextGO;
    GameObject TitleGO;
    GameObject MusicMenu;
    bool canStart=false;
    [HideInInspector] public bool musicIsSelected = false;
    void Start()
    {
        TitleGO = GameObject.Find("TitleScreenPanel").transform.Find("Title").gameObject;
        MusicMenu = Resources.Load<GameObject>("MusicMenu");
        TitleScreenTextGO = GameObject.Find("TitleScreenPanel").transform.Find("Text").gameObject;
        TitleScreenTextGO.SetActive(false);
        TitleGO.SetActive(false);
        StarterTextsContents = new List<string>();
        StarterTexts = new List<Text>();
        Transform StarterTextsParent = gameObject.transform.Find("Texts");
        foreach(Transform childs in StarterTextsParent)
        {
            StarterTextsContents.Add(childs.GetComponent<Text>().text);
            StarterTexts.Add(childs.GetComponent<Text>());
            childs.GetComponent<Text>().text = "";
        }
        TwinGearsLogo = transform.Find("TwinGearsLogo").GetComponent<Image>();

        StartCoroutine(StartMainMenuCorout());
    }

    IEnumerator StartMainMenuCorout()
    {
        TwinGearsLogo.CrossFadeAlpha(0, 0, true);

        yield return new WaitForSeconds(0.3f);
        TwinGearsLogo.CrossFadeAlpha(1, 1.5f, true);
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < StarterTexts.Count; i++)
        {
            int textIndex = 0;
            while(StarterTextsContents[i] != StarterTexts[i].text)
            {
                StarterTexts[i].text += StarterTextsContents[i][textIndex];
                textIndex++;
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < StarterTexts.Count; i++)
        {

            while (StarterTexts[i].text.Length != 0 )
            {
                StarterTexts[i].text = StarterTexts[i].text.Remove(StarterTexts[i].text.Length-1);
                yield return new WaitForSeconds(0.01f);
            }
        }
        TwinGearsLogo.CrossFadeAlpha(0, 0.5f, true);
        yield return new WaitForSeconds(0.5f);
        TitleScreenTextGO.SetActive(true);
        TitleGO.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        canStart = true;
    }

    private void Update()
    {
        if (canStart && Input.anyKeyDown)
        {
           this.LoadMusicMenu();
            canStart = false;
        }
        if (musicIsSelected)
        {
            StartCoroutine(NowLoading());
        }
    }

    private void LoadMusicMenu()
    {
        GameObject musicMenu = Instantiate(MusicMenu, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        musicMenu.transform.SetParent(GameObject.Find("Canvas").transform, false);
        TitleScreenTextGO.SetActive(false);
        TitleGO.SetActive(false);
    }
    IEnumerator NowLoading()
    {
        canStart = false;
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Game");
    }
}
