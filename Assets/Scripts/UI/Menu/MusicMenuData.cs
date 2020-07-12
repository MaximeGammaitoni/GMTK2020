using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;


public struct MusicData
{
    public string Title;
    public string Artiste;
    public string Difficulty;
    public string Score;

    public MusicData(string title, string artiste, string difficulty, string score)
    {
        Title = title;
        Artiste = artiste;
        Difficulty = difficulty;
        Score = score;
    }
}

public class MusicMenuData : MonoBehaviour
{
    public RecyclingListView theList;
    private List<MusicData> data = new List<MusicData>();
    private string jsonPath = "Levels";
    private Dictionary<string, Level> LevelsListTemplate { get; set; }

    private void Start()
    {
        theList.ItemCallback = PopulateItem;

        RetrieveData();

        // This will resize the list and cause callbacks to PopulateItem for
        // items that are needed for the view
        theList.RowCount = data.Count;
    }

    private void RetrieveData()
    {
        data.Clear();
        Dictionary<string, Level> levelList = GetLevelList();
        foreach(KeyValuePair<string, Level> level in levelList)
        {
            data.Add(new MusicData(level.Value.Name, level.Value.artistName, level.Value.Difficulty.ToString(), level.Value.Score.ToString()));
        }
    }

    public Dictionary<string, Level> GetLevelList()
    {
        var jsonTextFile = Resources.Load<TextAsset>(jsonPath);
       return JsonConvert.DeserializeObject<Dictionary<string, Level>>(jsonTextFile.text);
    }

    private void PopulateItem(RecyclingListViewItem item, int rowIndex)
    {
        var child = item as MusicMenuItems;
        child.ChildData = data[rowIndex];
    }
}
