using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AffectedGameObject;
    public List<GameObject> WarpPoints;
    int WarpPointIndex = 0;

    private void Start()
    {
        Warp();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            Warp();
        }
        
    }

    void Warp()
    {
        AffectedGameObject.transform.position = WarpPoints[WarpPointIndex].transform.position;
        AffectedGameObject.transform.rotation = WarpPoints[WarpPointIndex].transform.rotation;
        WarpPointIndex = (WarpPointIndex+1)%WarpPoints.Count;
    }
}
