using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RythmTool : MonoBehaviour
{
    [HideInInspector] public AudioSource AS;
    public List<int> RankData;
    public AudioClip LevelClip; 
    public int Difficulty;
    private float timer = 0;
    private List<float> data;
    private Text debugText;
    void Start()
    {
        debugText = GameObject.Find("Debug").GetComponent<Text>();
        AS = gameObject.GetComponent<AudioSource>();

        data = new List<float>();
        StartCoroutine(Begin());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator Begin() 
    {
        while (LevelClip.loadState != AudioDataLoadState.Loaded)
        {
            yield return 0;
        }
        AS.clip = LevelClip;
        
       
        debugText.text = "can start";

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return 0;
        }
        AS.Play();
        yield return 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                debugText.text = timer + "";
                data.Add(timer);
            }
            else if (Input.GetKeyDown(KeyCode.RightShift))
            {
                debugText.text = "data saved";
                Level level = new Level
                {
                    Name = LevelClip.name,
                    Difficulty = this.Difficulty,
                    Data = data,
                    Duration = LevelClip.length,
                    RankData = this.RankData,
                    Score = 0,
                    artistName = "O.SAN",
                };
                //transform into json 
                Dictionary<string, Level> dict = new Dictionary<string, Level>();
                dict.Add(LevelClip.name, level);
                JObject json =JObject.Parse(JsonConvert.SerializeObject(dict));
                Debug.Log(json);
                JObject fullData = JObject.Parse (File.ReadAllText(Application.dataPath + "/Resources/Levels.json"));
                
                fullData.Merge(json, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Concat
                });
                File.WriteAllText(Application.dataPath + "/Resources/Levels.json",fullData.ToString());
                //load into json
            }
            yield return 0;
        }
    }
}
