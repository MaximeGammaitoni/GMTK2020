using Newtonsoft.Json;
using States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderManager 
{
    public GameObject GameOverPanel;
    public GameObject Bar;
    public GameObject NotePrefab;
    public GameObject CirclePrefab;
    public AudioSource AudioSource;
    public AudioClip CurrentClip;
    public static Dictionary<string, Level> LevelsListTemplate { get; set; }
    public string levelId = "";
    public Level CurrentLevel;
    private string jsonPath = "Levels";
    private float timer = 0f;

    public LevelBuilderManager()
    {
        GameOverPanel = GameObject.Find("GameOverPanel");
        GameOverPanel.SetActive(false);
      
        if(GameManager.NextLevelId == null)
        {
            levelId = "Mastering";
        }
        else {
            levelId = GameManager.NextLevelId;
        }
        Bar = GameObject.Find("Bar");
        NotePrefab = Resources.Load<GameObject>("Note");
        CirclePrefab = Resources.Load<GameObject>("Circle");
        AudioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        CurrentClip = Resources.Load<AudioClip>("Audio/" + levelId);
        AudioSource.clip = CurrentClip;

        LevelsListTemplate = new Dictionary<string, Level>();
        CreateLevelsList();
        foreach (var level in LevelsListTemplate)
        {
            if (levelId == level.Key)
            {
                CurrentLevel = level.Value;
            }
        }
        GameManager.singleton.StartCoroutine(Run());

    }
    public void CreateLevelsList()
    {
        var jsonTextFile = Resources.Load<TextAsset>(jsonPath);
        LevelsListTemplate = JsonConvert.DeserializeObject<Dictionary<string, Level>>(jsonTextFile.text);
    }

    public IEnumerator Run()
    {
        yield return new WaitForSeconds(3);
        while(CurrentClip.loadState != AudioDataLoadState.Loaded)
        {
            yield return 0;
        }
        GameManager.singleton.StatesManager.CurrentState = new Run();
        Debug.Log(CurrentLevel.Data);
        AudioSource.Play();
        int indexNote = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >=  CurrentLevel.Data[indexNote])
            {
                GameManager.singleton.ObjectPullingManager.Pop(new Vector3(1, -0.353f, 0.974f));
                indexNote++;
            }
            
            yield return 0;
        }
       // yield return 0;
    }
}
