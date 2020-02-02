using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int ScoreToLevelUp { get; } = 1;
    private int CurrentScore { get; set; }
    private int CurrentLevel { get; set; } = 1;
    private float CurrentTime { get; set; }
    private bool GameEnded { get; set; }

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
            obj.Collect(playerHead.transform.GetChild(0).gameObject, CurrentLevel);
            CurrentScore += 1;

            if (CurrentLevel == 5)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                StartCoroutine(EndGame());

                var planet = GameObject.FindWithTag("Planet");
                planet.GetComponent<CollectibleRandomizer>().Collect(playerHead.gameObject, -transform.up, transform.right);
                GetComponent<RigidBodyFPSWalker>().CollectPlanet();
                
                var localPosition = cameraController.transform.localPosition;
                localPosition.z *= 4f;
                localPosition.y *= 2f;
                cameraController.SetDesiredLocalPosition(localPosition);
            }
            if (CurrentScore > ScoreToLevelUp)
            {
                CurrentLevel++;
                CurrentScore = 0;

                var localPosition = cameraController.transform.localPosition;
                localPosition.z *= 1.75f;
                localPosition.y *= 1.75f;
                cameraController.SetDesiredLocalPosition(localPosition);
                
                playerHead.SetDesiredLocalPosition(3f * playerHead.transform.localScale);
                GetComponent<Rigidbody>().mass = 10 * CurrentLevel;
                
            }
            else
                playerHead.SetDesiredLocalPosition(1.01f * playerHead.transform.localScale);
        }
    }

    private IEnumerator EndGame()
    {
        finalScore = CurrentTime;
        GameEnded = true;
        yield return new WaitForSeconds(3f);
        
        var localPosition = cameraController.transform.localPosition;
        localPosition.z /= 2f;
        localPosition.y /= 2f;
        cameraController.SetDesiredLocalPosition(localPosition);
        
        yield return new WaitForSeconds(2f);
        OpenLeaderboard();
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
        gastroLevel.text = $"Gastro Level: {CurrentLevel}";
        nextLevel.text = $"Next Level: {(ScoreToLevelUp - CurrentScore) + 1}";
        
        if (!GameEnded)
            timerText.text = $"Timer: {((float)Math.Round(CurrentTime *10f) / 10f).ToString("F1")}";
    }

    private void OpenLeaderboard()
    {
        //play end animation
        //open leaderboard
        leaderboard.LoadLeaderboard(finalScore);
        
    }
}
