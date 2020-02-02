using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int ScoreToLevelUp { get; } = 20;
    private int CurrentScore { get; set; }
    private int CurrentLevel { get; set; } = 1;
    private float CurrentTime { get; set; }

    private float finalScore;
    public LeaderboardGUI leaderboard;

    [SerializeField] private HeadSizeController playerHead;
    [SerializeField] private CameraController cameraController;

    [SerializeField] private Text gastroLevel;
    [SerializeField] private Text nextLevel;
    [SerializeField] private Text timerText;

    public void OnCollisionEnter(Collision other)
    {
        Collectable obj;
        if ((obj = other.collider.GetComponent<Collectable>()) != null && !obj.Collected && obj.levelNeededToCollect <= CurrentLevel)
        {
            obj.Collect(gameObject);
            CurrentScore += 1;

            if (CurrentScore > ScoreToLevelUp)
            {
                CurrentLevel++;
                CurrentScore = 0;

                var localPosition = cameraController.transform.localPosition;
                localPosition.z *= 1.25f;
                localPosition.y *= 1.25f;
                cameraController.SetDesiredLocalPosition(localPosition);
                
                playerHead.SetDesiredLocalPosition(1.2f * playerHead.transform.localScale);
            }
            else
                playerHead.SetDesiredLocalPosition(1.2f * playerHead.transform.localScale);
        }
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
        gastroLevel.text = $"Gastro Level: {CurrentLevel}";
        nextLevel.text = $"Next Level: {(ScoreToLevelUp - CurrentScore) + 1}";
        timerText.text = $"Timer: {((float)Math.Round(CurrentTime *10f) / 10f).ToString("F1")}";
    }

    public void OpenLeaderboard()
    {
        finalScore = CurrentTime;
        //play end animation
        //open leaderboard
        leaderboard.LoadLeaderboard(finalScore);
        
    }
}
