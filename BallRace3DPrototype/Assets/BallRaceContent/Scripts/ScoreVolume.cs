using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreVolume : MonoBehaviour
{
    public ScoreManager scoreManagerComponent;

    public int VolumeScoreWorth;


    #region Location Variables;
    int CurrentLocationID = -1;
    #endregion


    private void Update()
    {
        if(CurrentLocationID < 0)
        {
            SwapToNewLocation();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int PlayerID = other.gameObject.GetComponent<PlayerMovement>().playerID;

            scoreManagerComponent.addScore(PlayerID, VolumeScoreWorth);
            SwapToNewLocation();
        }
    }

    void SwapToNewLocation()
    {
        if(scoreManagerComponent.PossibleScoreLocations.Count > 1)
        {
            int WarpTarget = Random.Range(0, scoreManagerComponent.PossibleScoreLocations.Count);
            
            while(scoreManagerComponent.scoreLocationFull(WarpTarget))
            {
                WarpTarget = Random.Range(0, scoreManagerComponent.PossibleScoreLocations.Count);
            }
            scoreManagerComponent.RemoveFromUsedLocations(CurrentLocationID);
            CurrentLocationID = WarpTarget;
            scoreManagerComponent.AddToUsedLocations(CurrentLocationID);

            WarpToTarget(scoreManagerComponent.PossibleScoreLocations[WarpTarget]);

        }
        else
        {
            Debug.LogWarning("Not Enough Spawn Points To Warp");
        }
    }


    void WarpToTarget(Transform _target)
    {
        this.transform.position = _target.position;
        this.transform.rotation = _target.rotation;
        this.transform.localScale = _target.localScale;
    }

}
