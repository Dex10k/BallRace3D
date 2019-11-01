using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    public int[] PlayerScore = new int[4];

    public Text Player1ScoreDisplay;
    public Text Player2ScoreDisplay;
    public Text Player3ScoreDisplay;
    public Text Player4ScoreDisplay;

    Text[] PlayerScoreDisplay = new Text[4];

    [Space(10)]
    [Header("SpawnLocations")]
    public List<Transform> PossibleScoreLocations;

    List<int> UsedScoreLocations;

    [Space(20)]
    [Header("Material Lead Properties")]

    public Material WallMaterial;
    public Material GroundMaterial;

    public Color[] PlayerWallColours;
    public Color[] PlayerGroundColours;

    private ScoreVisual[] scoreImages;

    private void Start()
    {
        UsedScoreLocations = new List<int>();
        //PossibleScoreLocations = new List<Transform>() ;

        PlayerScoreDisplay[0] = Player1ScoreDisplay;
        PlayerScoreDisplay[1] = Player2ScoreDisplay;
        PlayerScoreDisplay[2] = Player3ScoreDisplay;
        PlayerScoreDisplay[3] = Player4ScoreDisplay;

        scoreImages = GetComponentsInChildren<ScoreVisual>();

    }


    // Update is called once per frame
    void Update()
    {
        UpdateUIElements();
        ChangeStageColour();
    }

    #region Score Functions
    public void addScore(int PlayerID, int ScoreIncrease)
    {
       
            PlayerScore[PlayerID] += ScoreIncrease;

        scoreImages[PlayerID].OnScoreIncrease();

    }



    void UpdateUIElements()
    {
        for (int _i = 0; _i < PlayerScore.Length; _i++)
        {
            PlayerScoreDisplay[_i].text = ("" + PlayerScore[_i]);
         
        }        
    }

    #endregion

    #region Adjust Score Location
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

    #endregion

    #region Aesthetic Functions

    void ChangeStageColour()
    {
        int CurrentHighestScore = 0;

        for (int _i = 0; _i < PlayerScore.Length; _i++)
        {
             if(PlayerScore[_i] > CurrentHighestScore) { CurrentHighestScore = PlayerScore[_i]; }
        }

        int numberOfPlayersInTheLead = 0;
        int LeadPlayerID = 5;

        for (int _i = 0; _i < PlayerScore.Length; _i++)
        {
            if(PlayerScore[_i] == CurrentHighestScore)
            {
                numberOfPlayersInTheLead++;
                LeadPlayerID = _i;
            }
        }

        if(numberOfPlayersInTheLead <= 1)
        {
            WallMaterial.SetColor("_BaseColor", PlayerWallColours[LeadPlayerID]);
            
            GroundMaterial.SetColor("_BaseColor", PlayerGroundColours[LeadPlayerID]);
            
        }
        else if(numberOfPlayersInTheLead > 1)
        {
            WallMaterial.SetColor("_BaseColor", PlayerWallColours[4]);           
            GroundMaterial.SetColor("_BaseColor", PlayerGroundColours[4]);            
        }

    }

    #endregion

}
