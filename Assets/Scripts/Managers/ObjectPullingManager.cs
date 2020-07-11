using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPullingManager
{
    public List<GameObject> GameObjectsList;
    public GameObject GObjectParent;
    public GameObject GObjectPrefab;
    public int amountToPool = 100;
    public ObjectPullingManager()
    {
        GObjectParent = new GameObject("PullingParent");
        GObjectPrefab = Resources.Load("Note") as GameObject;
        GameObjectsList = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)GameManager.Instantiate(GObjectPrefab);
            obj.SetActive(false);
            obj.name = "PullableObject";
            obj.transform.parent = GObjectParent.transform;
            GameObjectsList.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < GameObjectsList.Count; i++)
        {
            if (!GameObjectsList[i].activeInHierarchy)
            {
                return GameObjectsList[i];
            }
        }
        GameObject obj = (GameObject)GameManager.Instantiate(GObjectPrefab);
        obj.SetActive(false);
        obj.name = "PullableObject";
        obj.transform.parent = GObjectParent.transform;
        GameObjectsList.Add(obj);
        return obj;
    }
    public void Pop(Vector3 pos)
    {
        GameObject go = GetObject();
        go.SetActive(true);
        go.transform.position = pos;
    }
}

