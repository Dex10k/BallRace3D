using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowOffsetTarget : MonoBehaviour
{

    public Transform CameraFollowOffsetObject;
    public Transform PlayerObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        this.transform.position = PlayerObject.position + CameraFollowOffsetObject.position;
    }
}
