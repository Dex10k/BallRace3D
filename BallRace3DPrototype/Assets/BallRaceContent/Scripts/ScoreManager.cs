using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int Player1Score = 0;
    int Player2Score = 0;
    int Player3Score = 0;
    int Player4Score = 0;

    public Text Player1ScoreDisplay;
    public Text Player2ScoreDisplay;
    public Text Player3ScoreDisplay;
    public Text Player4ScoreDisplay;


    [Space(10)]
    [Header("SpawnLocations")]
    public List<Transform> PossibleScoreLocations;

    List<int> UsedScoreLocations;


    private void Start()
    {
        UsedScoreLocations = new List<int>();
        //PossibleScoreLocations = new List<Transform>() ;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateUIElements();
    }

    public void addScore(int PlayerID, int ScoreIncrease)
    {
        if(PlayerID == 0)
        {
            Player1Score += ScoreIncrease;
        }
        else if (PlayerID == 1)
        {
            Player2Score += ScoreIncrease;
        }
        else if (PlayerID == 2)
        {
            Player3Score += ScoreIncrease;
        }
        else if (PlayerID == 3)
        {
            Player4Score += ScoreIncrease;
        }
    }



    void UpdateUIElements()
    {
        Player1ScoreDisplay.text = ("" +Player1Score);
        Player2ScoreDisplay.text = ("" + Player2Score);
        Player3ScoreDisplay.text = ("" + Player3Score);
        Player4ScoreDisplay.text = ("" + Player4Score);
    }


    public bool scoreLocationFull(int scoreLocationToCheck)
    {
        if(UsedScoreLocations.Count >= PossibleScoreLocations.Count)
        {
            Debug.LogError("All Score Volumes Are Full");
            return false;
        }


        for(int _i = 0; _i < UsedScoreLocations.Count; _i++)
        {
            if(scoreLocationToCheck == UsedScoreLocations[_i])
            {
                return true;
            }
        }

        return false;
    }

    public void AddToUsedLocations(int locationID)
    {
        UsedScoreLocations.Add(locationID);
    }

    public void RemoveFromUsedLocations(int locationID)
    {
        UsedScoreLocations.Remove(locationID);
    }
}
