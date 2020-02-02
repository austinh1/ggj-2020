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
    private GameObject newHighScoreObj;
    public GameObject playerStats;

    public void Start()
    {
        //Leaderboard.DeleteAllEntries();
    }

    public void LoadLeaderboard(float score)
    {
        gameObject.SetActive(true);
        bool newHighScore = false;
        // Display high scores!
        for (int i = 0; i < Leaderboard.EntryCount; ++i)
        {
            var entry = Leaderboard.GetEntry(i);
            if (entry.score > score && !newHighScore)
            {
                highScoreMenu.SetActive(true);
                endMenu.SetActive(false);
                newHighScore = true;
                
                newHighScoreObj = Instantiate(scorePrefab, scoresParent.transform);
                ScorePrefab newScoreScript = newHighScoreObj.GetComponent<ScorePrefab>();
                newScoreScript.score.text = score.ToString("F2");
                newScoreScript.gameObject.SetActive(true);
            }
            GameObject highScore = Instantiate(scorePrefab, scoresParent.transform);
            ScorePrefab scoreScript = highScore.GetComponent<ScorePrefab>();
            scoreScript.name.text = entry.name;
            scoreScript.score.text = entry.score.ToString("F2");
            if (i < Leaderboard.EntryCount - 1 || !newHighScore)
            {
                scoreScript.gameObject.SetActive(true);
            }
            else if(newHighScore)
            {
                scoreScript.gameObject.SetActive(false);
            }
        }

        scoreText.text = score.ToString("F2");
    }

    public void SubmitScore()
    {
        float score;
        _scoreInput = scoreText.text;
        float.TryParse(_scoreInput, out score);
        Debug.Log("SCORE: " + score);
        _nameInput = nameInput.text;
        Leaderboard.Record(_nameInput, score);
        if (newHighScoreObj != null)
        {
            ScorePrefab newScore = newHighScoreObj.GetComponent<ScorePrefab>();
            newScore.name.text = _nameInput;
            newScore.name.color = Color.cyan;
            newScore.score.color = Color.cyan;
        }

        // Reset for next input.
        _nameInput = "";
        _scoreInput = "0";
        playerStats.SetActive(false);
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