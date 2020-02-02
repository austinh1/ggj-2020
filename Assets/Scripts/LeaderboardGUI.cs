using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardGUI : MonoBehaviour
{
    private string _nameInput = "";
    private string _scoreInput = "0";
    public TMP_Text scoreText;
    public TMP_InputField nameInput;
    public GameObject scorePrefab;
    public GameObject scoresParent;

    public GameObject highScoreMenu;
    public GameObject endMenu;

    public void Start()
    {
        //Leaderboard.DeleteAllEntries();
    }

    public void LoadLeaderboard(float score)
    {
        gameObject.SetActive(true);
        // Display high scores!
        for (int i = 0; i < Leaderboard.EntryCount; ++i)
        {
            var entry = Leaderboard.GetEntry(i);
            GameObject highScore = Instantiate(scorePrefab, scoresParent.transform);
            ScorePrefab scoreScript = highScore.GetComponent<ScorePrefab>();
            scoreScript.name.text = entry.name;
            scoreScript.score.text = entry.score.ToString();
            scoreScript.gameObject.SetActive(true);
            if (entry.score > score)
            {
                highScoreMenu.SetActive(true);
                endMenu.SetActive(false);
            }
            
        }

        scoreText.text = score.ToString();
    }

    public void SubmitScore()
    {
        float score;
        _scoreInput = scoreText.text;
        float.TryParse(_scoreInput, out score);
        Debug.Log("SCORE: " + score);
        _nameInput = nameInput.text;
        Leaderboard.Record(_nameInput, score);

        // Reset for next input.
        _nameInput = "";
        _scoreInput = "0";
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}