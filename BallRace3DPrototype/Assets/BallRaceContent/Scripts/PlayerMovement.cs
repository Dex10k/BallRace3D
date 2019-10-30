using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Player Properties

    Rigidbody rigidbodyComponent;
    [Header("Player Properties")]
    [Space(10)]
    [Range(1, 100)]
    public float BaseSpeed = 5f;
    [Range(1, 20)]
    public float MinSpeedForce = 5f;
    [Range(20, 500)]
    public float MaxSpeedForce = 100f;
    [Range(0,20)]

    [Space(10)]
    public float JumpStrength = 3f;

    [Space(10)]
    public string DestroyingObstacleTag;
    public Transform StartingCheckpoint;
    public LayerMask GroundLayer;
    [Range(0,1)]
    public float GroundCheckDistance =  0.2f;

    #endregion

    #region Bounce Properties
    [Header("Bounce Properties")]

    [Range(0, 5)]
    public float bounceSpeedIncrement = 0.3f;
    [Range(0, 15)]
    public float FrictionSlowPerSecond = 1f;

    [Space (20)]

    #endregion

    #region Camera Properties

    [Header ("Camera Properties")]
    
    public Transform CameraObject;

    [Range(1, 100)]
    public float CameraSensitivityHorizontal;
    [Range(1, 100)]
    public float CameraSensitivityVertical;
    public Transform CameraOffsetController;
    public Transform CameraOffsetDistanceController;

    public float CameraMaxHeight = 10;
    public float CameraMinHeight = 3;

    public float CameraMinimumDistance = 5;

    #endregion

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
        ManualCameraRotate();
        GroundFriction();
    }

    #region PlayerControlledFunctions

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
            
            
            if(GroundCheck()){
                Debug.Log("JumpAttempt");
                rigidbodyComponent.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
            }

           

        }
    }

    bool GroundCheck()
    {
        Ray GroundCheckDirection = new Ray(this.transform.position, Vector3.down);
        if (Physics.Raycast(GroundCheckDirection, GroundCheckDistance + this.transform.localScale.x, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region CameraCode

    void ManualCameraRotate()
    {
        if (Input.GetAxisRaw("AimHorizontal") != 0) {
            //Debug.Log("Aim Horizontal Returns : " + Input.GetAxisRaw("AimHorizontal"));
            CameraOffsetController.transform.Rotate(0, Input.GetAxis("AimHorizontal"),0,Space.Self);
         }

        if(Input.GetAxisRaw("AimVertical") != 0)
        {
            Vector3 NewOffsetPosition = CameraOffsetController.transform.position;

            NewOffsetPosition.y += (Input.GetAxisRaw("AimVertical") * CameraSensitivityVertical * Time.deltaTime *-1f);

            if (NewOffsetPosition.y < CameraMinHeight)
            {
                NewOffsetPosition.y = CameraMinHeight;
            } else if (NewOffsetPosition.y > CameraMaxHeight)
            {
                NewOffsetPosition.y = CameraMaxHeight;
            }

            

            CameraOffsetController.transform.position = NewOffsetPosition;


        }
        float CameraHeight = CameraOffsetController.position.y;
            CameraOffsetDistanceController.transform.localPosition = new Vector3(0, 0, -1 * ((CameraMaxHeight - CameraHeight) + CameraMinimumDistance) );
        
    }


    void CameraAutoCorrect()
    {
        Vector3 currentObjectVelocity = rigidbodyComponent.velocity;

        currentObjectVelocity.Normalize();



    }

    #endregion


    #region NonPlayerControlledElements


    void GroundFriction()
    {
        if(GroundCheck() & BaseSpeed > MinSpeedForce){

            if (BaseSpeed - FrictionSlowPerSecond * Time.deltaTime > MinSpeedForce)
            {
                BaseSpeed -= FrictionSlowPerSecond * Time.deltaTime;
            }
            else if (BaseSpeed - FrictionSlowPerSecond * Time.deltaTime <= MinSpeedForce)
            {
                BaseSpeed = MinSpeedForce;
            }

        }
    }

    void BounceSpeedUp()
    {
        Debug.Log("Is Bounce");
        if (BaseSpeed + bounceSpeedIncrement < MaxSpeedForce)
        {
            BaseSpeed += bounceSpeedIncrement;
        } else if (BaseSpeed + bounceSpeedIncrement > MaxSpeedForce)
        {
            BaseSpeed = MaxSpeedForce;
        }
    }





    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == DestroyingObstacleTag)
        {
            this.rigidbodyComponent.velocity = Vector3.zero;
            this.transform.position = StartingCheckpoint.position;
        } else if (LayermaskCollisionCast(GroundLayer,collision.gameObject))
        {
            BounceSpeedUp();
        }
    }


    bool LayermaskCollisionCast(LayerMask p_layerMask, GameObject p_Object)
    {
        if (p_layerMask == (p_layerMask | (1 << p_Object.layer)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion


}
