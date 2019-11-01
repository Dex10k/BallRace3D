using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField]

    int NumberOfLapsToEnd;

    int NumberOfPlayersFinished = 0;

    public int FirstPlaceScoreBonus;
    public int SecondPlaceScoreBonus;
    public int ThirdPlaceScoreBonus;
    public int FourthPlaceScoreBonus;

    int[] PlayerLapNumber = new int[4];
    bool[] PlayerHasMadeALap = new bool[4];

    public GameObject scoreManager;
    ScoreManager scoreManagerComponent;



    public void Awake()
    {
        scoreManagerComponent = scoreManager.GetComponent<ScoreManager>();
        for(int _i = 0; _i < PlayerHasMadeALap.Length; _i++)
        {
            PlayerHasMadeALap[_i] = true;
        }

    }

    public void LapComplete(int PlayerID)
    {
        if (PlayerHasMadeALap[PlayerID] == false)
        {
            PlayerLapNumber[PlayerID]++;
            PlayerHasMadeALap[PlayerID] = true;
        }

        if(PlayerLapNumber[PlayerID] >= NumberOfLapsToEnd)
        {
            CompletedRace(PlayerID);
        }
    }


    void CompletedRace(int PlayerID)
    {
        if(NumberOfPlayersFinished == 0)
        {
            scoreManagerComponent.addScore(PlayerID, FirstPlaceScoreBonus);
        } else if(NumberOfPlayersFinished == 1)
        {
            scoreManagerComponent.addScore(PlayerID, SecondPlaceScoreBonus);
        }
        else if (NumberOfPlayersFinished == 2)
        {
            scoreManagerComponent.addScore(PlayerID, ThirdPlaceScoreBonus);
        }
        else
        {
            scoreManagerComponent.addScore(PlayerID, FourthPlaceScoreBonus);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LapComplete(other.GetComponent<PlayerMovement>().playerID);

        }
    }


    public void PassedHalfWay(int PlayerID)
    {
        PlayerHasMadeALap[PlayerID] = true;
    }
}
