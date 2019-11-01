using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GrabScoreData : MonoBehaviour
{

    public ScoreManager scoreManager;


    public Text WinningPlayerDisplay;
    int[] PlayerScore = new int[4];

    public Text[] PlayerScoreDisplay = new Text[4];


    private void Start()
    {
        
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateScoreData()
    {
        
        int CurrentHighestScore = 0;
        int LeadPlayerID = 5;
        for (int _i = 0; _i < PlayerScore.Length; _i++)
        {
            PlayerScore[_i] = scoreManager.PlayerScore[_i];
        }

        for (int _i = 0; _i < PlayerScore.Length; _i++)
        {
            if (scoreManager.PlayerScore[_i] >= CurrentHighestScore)
            {
                CurrentHighestScore = scoreManager.PlayerScore[_i];
                LeadPlayerID = _i;
            }
        }

        for (int _i = 0; _i < PlayerScore.Length; _i++)
        {
            PlayerScoreDisplay[_i].text = ("" + PlayerScore[_i]);

        }

        WinningPlayerDisplay.text = ("Player " + (LeadPlayerID + 1) + " Wins");
        if (PlayerScoreDisplay[LeadPlayerID].color != null)
        {
            WinningPlayerDisplay.color = PlayerScoreDisplay[LeadPlayerID].color;
        }



    }

    void UpdateUIElements()
    {
        
    }
}
