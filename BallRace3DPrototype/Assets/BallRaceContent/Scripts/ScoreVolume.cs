using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreVolume : MonoBehaviour
{
    public ScoreManager scoreManagerComponent;

    public int VolumeScoreWorth;

    public int Cooldown;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int PlayerID = other.gameObject.GetComponent<PlayerMovement>().playerID;

            scoreManagerComponent.addScore(PlayerID, VolumeScoreWorth);
        }
    }


 
}
