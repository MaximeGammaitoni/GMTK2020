using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMusicAction : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public void OnClick()
    {
        GameManager.NextLevelId = gameObject.transform.parent.GetComponent<MusicMenuItems>().levelId;
       GameObject.Find("StarterPanel").GetComponent<MainMenuStarter>().musicIsSelected = true;
    }
}
