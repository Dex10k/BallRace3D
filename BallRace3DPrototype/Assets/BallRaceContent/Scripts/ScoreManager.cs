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




    // Start is called before the first frame update
    void Start()
    {
        
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
}
