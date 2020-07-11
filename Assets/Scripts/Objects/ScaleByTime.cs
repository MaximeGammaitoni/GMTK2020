using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleByTime : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(Update());
    }
    private IEnumerator Update()
    {
        Vector3 target = Vector3.one * 0.5f;
        float ratio = 0.00f;
        float speed = 0.01f;
        while (transform.localScale != target )
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, target, ratio += Time.deltaTime / 2 * GameplayConfig.TimeScale * speed);
            
            yield return 0;
        }
        ratio = 0f;
        speed = 0.02f;
        while (transform.GetComponent<SpriteRenderer>().color != Color.white)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.LerpUnclamped(transform.GetComponent<SpriteRenderer>().color, Color.white, ratio += Time.deltaTime / 2 * GameplayConfig.TimeScale * speed *70);
            yield return 0;
        }

        Destroy(gameObject);
    }
}
