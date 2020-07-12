using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerEvents
{

    

    public PlayerEvents()
    {
        OnPlayerDeath += PlayerDeath;
        EventsManager.StartListening("PlayerDeath", OnPlayerDeath);
        PlayerIsDead();
    }

    public UnityAction<Args> OnPlayerDeath;
    public class PlayerDeathArgs : Args
    {
        public GameObject PlayerGo;
    }
    private void PlayerDeath(Args args)
    {
        if (args.GetType() != typeof(PlayerEvents.PlayerDeathArgs))
            throw new Exception("argument must be a PlayerDeathArgs");

        GameManager.singleton.StartCoroutine(GameOverEffect());


    }
    public void PlayerIsDead()
    {
        EventsManager.TriggerEvent("PlayerDeath", new PlayerDeathArgs { PlayerGo = new GameObject("test") });
        
    }

    IEnumerator GameOverEffect()
    {
        GameManager.singleton.LevelBuilderManager.GameOverPanel.SetActive(true);
        AudioSource sound = GameManager.singleton.gameObject.GetComponent<AudioSource>();
        while (sound.volume > 0)
        {
            sound.volume -= Time.deltaTime * 2f;

            yield return 0;
        }
        yield return new WaitForSeconds(8f);
        sound.Stop();
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
