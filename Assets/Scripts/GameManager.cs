using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int killCount;

    public static GameManager instance;

    public Transform projectileParent;
    public Transform player_Parent;
    public GameObject player_Item;
    public GameObject player_instance;
    bool processingPlayer;

    public int score, kills, lives;

    [Header("Ui")]
    public UIContainer uiContainer;

    void OnEnable()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void OnDisable()
    {
        instance = null;
    }

    
    public void OnPlayerDeath(Vector3 position)
    {
        if (processingPlayer)
        {
            return;
        }
        processingPlayer = true;
        StartCoroutine(Utilities.CallAfter(0.2f, () =>
        {
            GameObject obj = Instantiate(player_Item, player_Parent);
            obj.transform.position = position;
            player_instance = obj;
            processingPlayer = false;
            lives -= 1;
            UpdateHud();
        }));
    }

    public void OnEnemyKilled()
    {
        score += 100;
        kills += 1;
        UpdateHud();
    }

    void UpdateHud()
    {
        uiContainer.score.text = "Score : " + score.ToString();
        uiContainer.lives.text = "Lives : " + lives.ToString();
    }

    public void OnBossKilled()
    {
        OnGameComplete();
    }

    void OnGameComplete()
    {
        uiContainer.gameOverPanel.SetActive(true);
        uiContainer.finalScore.text = "final score : " + score.ToString();
        uiContainer.livesRemaining.text = "lives remaining : " + lives.ToString();
        uiContainer.killedEnemies.text = "enemies killed : " + kills.ToString();
    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene(0);
    }

}

[Serializable]
public class UIContainer
{
    [Header("hud")]
    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI livesRemaining;
    public TextMeshProUGUI killedEnemies;
}

public static class Utilities
{

    public static IEnumerator CallAfter(float seconds, Action callBack)
    {
        yield return new WaitForSeconds(seconds);
        callBack();
    }

}