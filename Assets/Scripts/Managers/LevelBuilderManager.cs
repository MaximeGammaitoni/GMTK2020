using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using States;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBuilderManager 
{
    public GameObject GameOverPanel;
    public GameObject WinPanel;
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
        WinPanel = GameObject.Find("WinPanel");

        GameOverPanel.SetActive(false);
        WinPanel.SetActive(false);

        if (GameManager.NextLevelId == null)
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
                if (indexNote > CurrentLevel.Data.Count -1)
                {
                    GameManager.singleton.StartCoroutine(Win());
                }
            }
            
            yield return 0;
        }
    }

    IEnumerator Win()
    {

        if(GameManager.singleton.ScoreManager.scoreValue > CurrentLevel.Score)
        {

            CurrentLevel.Score = GameManager.singleton.ScoreManager.scoreValue;
            JObject json = JObject.Parse(JsonConvert.SerializeObject(LevelsListTemplate));
            Dictionary<string, Level> dic = new Dictionary<string, Level>();
            dic.Add(GameManager.NextLevelId, CurrentLevel);
            json.Merge(dic, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });
            File.WriteAllText(Application.dataPath + "/Resources/Levels.json", json.ToString());
            
        }
        WinPanel.SetActive(true);
        WinPanel.transform.Find("Score").GetComponent<Text>().text = GameManager.singleton.ScoreManager.scoreValue.ToString();
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync("MainMenu");
        yield return 0;
    }
}
