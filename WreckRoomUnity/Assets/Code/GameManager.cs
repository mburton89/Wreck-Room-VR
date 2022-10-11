using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance;

    int score;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] AudioSource gameOverSound;

    void Awake()
    {
        Instance = this;
    }

    public void HandleEnemyHit()
    {
        score++;
        scoreText.SetText(score.ToString());
        AIBotManager.Instance.SpawnBot();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCo());
    }

    private IEnumerator GameOverCo()
    {
        //gameOverSound.Play();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
