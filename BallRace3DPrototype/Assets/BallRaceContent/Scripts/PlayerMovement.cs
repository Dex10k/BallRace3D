using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    Rigidbody rigidbodyComponent;
    [Range(1, 100)]
    public float BaseSpeed = 5f;
    [Range(0,20)]
    public float JumpStrength = 3f;

    public string DestroyingObstacleTag;
    public Transform StartPoint;

    public Transform CameraObject;

    public LayerMask GroundLayer;
    [Range(0,1)]
    public float GroundCheckDistance =  0.2f;


    [Range(1, 100)]
    public float CameraSensitivity;
    public Transform CameraOffsetController;

    public float CameraMaxHeight = 20;
    public float CameraMinHeight = 1;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        Jump();
        CameraRotate();
    }


    void HorizontalMovement()
    {
        Vector3 MoveSpeed = Vector3.zero;

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            MoveSpeed += (new Vector3(Input.GetAxis("Horizontal"), 0, 0) * BaseSpeed);
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            MoveSpeed += (new Vector3(0, 0, Input.GetAxis("Vertical")) * BaseSpeed);
        }

        //Vector3 CameraForward = CameraObject.position - this.transform.position;

        //CameraForward.y = 0f;

        MoveSpeed = CameraObject.TransformDirection(MoveSpeed);
        MoveSpeed.y = 0f;



        rigidbodyComponent.AddForce(MoveSpeed, ForceMode.Acceleration);
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            
            Ray GroundCheckDirection = new Ray(this.transform.position ,Vector3.down);
            RaycastHit Hit;

            if(Physics.Raycast(GroundCheckDirection, GroundCheckDistance + this.transform.localScale.x, GroundLayer)){
                Debug.Log("JumpAttempt");
                rigidbodyComponent.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
            }

           

        }
    }

    void CameraRotate()
    {
        if (Input.GetAxisRaw("AimHorizontal") != 0) {
            //Debug.Log("Aim Horizontal Returns : " + Input.GetAxisRaw("AimHorizontal"));
            CameraOffsetController.transform.Rotate(0, Input.GetAxis("AimHorizontal"),0,Space.Self);
         }

        if(Input.GetAxisRaw("AimVertical") != 0)
        {
            Vector3 NewOffsetPosition = CameraOffsetController.transform.position;

            NewOffsetPosition.y += (Input.GetAxisRaw("AimVertical") * CameraSensitivity/2 * Time.deltaTime);

            if (NewOffsetPosition.y < CameraMinHeight)
            {
                NewOffsetPosition.y = CameraMinHeight;
            } else if (NewOffsetPosition.y > CameraMaxHeight)
            {
                NewOffsetPosition.y = CameraMaxHeight;
            }


            CameraOffsetController.transform.position = NewOffsetPosition;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == DestroyingObstacleTag)
        {
            this.rigidbodyComponent.velocity = Vector3.zero;
            this.transform.position = StartPoint.position;
        }
    }
}
