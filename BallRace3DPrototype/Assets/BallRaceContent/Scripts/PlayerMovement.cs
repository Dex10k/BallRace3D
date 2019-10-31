using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

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

    [Range(0, 30)]
    public float bounceSpeedIncrement = 0.3f;
    [Range(0, 15)]
    public float FrictionSlowPerSecond = 1f;
    [Range(0,100)]
    public float percentageOfBaseSpeedToAddToSlow = 5f;

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
    [Space(5)]
    [Range(1, 20)]
    public float MinSpeedForAutoCorrect = 5;
    [Range(0,180)]
    public float CameraAutocorrectAngleChangePerSecond = 5f;

    #endregion

    private Player playerInput;

    public int playerID;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = ReInput.players.GetPlayer(playerID);

        rigidbodyComponent = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        Jump();
        //ManualCameraRotate();
        //CameraAutoCorrect();
        GroundFriction();
    }

    #region PlayerControlledFunctions

    void HorizontalMovement()
    {
        Vector3 MoveSpeed = Vector3.zero;


        if(playerInput.GetAxisRaw("MoveHorizontal") != 0)
        {
            MoveSpeed += (new Vector3(playerInput.GetAxis("MoveHorizontal"), 0, 0) * BaseSpeed);
        }
        if (playerInput.GetAxisRaw("MoveVertical") != 0)
        {
            MoveSpeed += (new Vector3(0, 0, playerInput.GetAxis("MoveVertical")) * BaseSpeed);
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
        if (playerInput.GetAxisRaw("AimHorizontal") != 0) {
            //Debug.Log("Aim Horizontal Returns : " + Input.GetAxisRaw("AimHorizontal"));
            CameraOffsetController.transform.Rotate(0, playerInput.GetAxis("AimHorizontal"),0,Space.Self);
         }

        if(playerInput.GetAxisRaw("AimVertical") != 0)
        {
            Vector3 NewOffsetPosition = CameraOffsetController.transform.position;

            NewOffsetPosition.y += (playerInput.GetAxisRaw("AimVertical") * CameraSensitivityVertical * Time.deltaTime *-1f);

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

        if (currentObjectVelocity.magnitude >= MinSpeedForAutoCorrect)
        {

            currentObjectVelocity.y = 0;

            currentObjectVelocity.Normalize();

            Vector3 CurrentCameraAngle = CameraOffsetController.transform.forward;

            float RadianChangePerSecond = CameraAutocorrectAngleChangePerSecond * Mathf.Deg2Rad * Time.deltaTime;

            //Vector3.RotateTowards(CurrentCameraAngle, currentObjectVelocity, RadianChangePerSecond, 0f);

            CameraOffsetController.transform.forward = Vector3.RotateTowards(CurrentCameraAngle, currentObjectVelocity, RadianChangePerSecond, 0f);
        }

    }

    #endregion


    #region NonPlayerControlledElements


    void GroundFriction()
    {
        if(GroundCheck() & BaseSpeed > MinSpeedForce){

            if (BaseSpeed - FrictionSlowPerSecond * Time.deltaTime > MinSpeedForce)
            {
                BaseSpeed -= (FrictionSlowPerSecond + ((BaseSpeed - MinSpeedForce)  * (0.01f * percentageOfBaseSpeedToAddToSlow))) * Time.deltaTime;
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
