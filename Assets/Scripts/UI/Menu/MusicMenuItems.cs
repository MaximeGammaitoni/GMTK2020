using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicMenuItems : RecyclingListViewItem
{
    public Text leftTextTop;
    public Text leftTextBottom;
    public Text rightTextTop;
    public Text rightTextBottom;

    private MusicData childData;
    public MusicData ChildData
    {
        get { return childData; }
        set
        {
            childData = value;
            leftTextTop.text = childData.Title;
            leftTextBottom.text = childData.Artiste;
            rightTextTop.text = childData.Difficulty;
            rightTextBottom.text = childData.Score;
        }
    }
}
