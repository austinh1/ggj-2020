using System;
using System.Collections;
using TMPro;
using UnityEngine;
 
public class LeaderboardGUI : MonoBehaviour {
    private string _nameInput = "";
    private string _scoreInput = "0";
    public TMP_Text scoreText;
    public TMP_InputField nameInput;
    public GameObject scorePrefab;
    public GameObject scoresParent;
    
    private void Start()
    {
        //Leaderboard.DeleteEntry(0);
        LoadLeaderboard(1000);
    }

    public void LoadLeaderboard(float score)
    {
        gameObject.SetActive(true);
        // Display high scores!
        for (int i = 0; i < Leaderboard.EntryCount; ++i) {
            var entry = Leaderboard.GetEntry(i);
            GameObject highScore = Instantiate(scorePrefab, scoresParent.transform);
            highScore.GetComponent<TMP_Text>().text = "Name: " + entry.name + ", Score: " + entry.score;
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
}