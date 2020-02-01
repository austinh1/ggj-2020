using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int ScoreToLevelUp { get; set; } = 10;
    private int CurrentScore { get; set; }
    private int CurrentLevel { get; set; } = 1;
    public void OnCollisionEnter(Collision other)
    {
        Collectable obj;
        if ((obj = other.collider.GetComponent<Collectable>()) != null && obj.levelNeededToCollect <= CurrentLevel)
        {
            obj.Collect(gameObject);
            CurrentScore += 1;

            if (CurrentScore > ScoreToLevelUp)
            {
                CurrentLevel++;
                CurrentScore = 0;
            }
        }
    }
}
