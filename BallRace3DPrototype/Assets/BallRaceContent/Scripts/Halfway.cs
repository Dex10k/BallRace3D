using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halfway : MonoBehaviour
{

    WinCondition winConditionComponent;

    // Start is called before the first frame update
    void Start()
    {
        winConditionComponent = this.gameObject.GetComponentInParent<WinCondition>(); 
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            winConditionComponent.PassedHalfWay(other.GetComponent<PlayerMovement>().playerID);
        }
    }
}
