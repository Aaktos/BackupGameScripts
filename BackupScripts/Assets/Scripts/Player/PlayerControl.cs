using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    CharacterController controller;
    Collider playerCollider;
    Vector3 strafeDirection;
    Vector3 moveDirection;
    Vector3 mousePosition;
    Vector3 lastDirection;
    Quaternion lookRotation;
    PlayerCameraScript cameraScript;

    public float walkSpeed;
    public float strafeSpeed;
    public float jumpSpeed;
    public float gravity;
    public float lookSpeed;

    bool pause; //handles pausing and unpausing off player controls

    float distToGround;
    float verticalSpeed;
    float mouseXLook = 0.0f;

   

    void Start () {
        playerCollider = GetComponent<Collider>();
        controller = GetComponent<CharacterController>();
        cameraScript = GetComponent<PlayerCameraScript>();
  
        distToGround = playerCollider.bounds.extents.y;// distance from the centre of the player collider to the bottom/top
    }
	
    void FixedUpdate()
    {   
        
            Movement();
        

         
    }

 //MOVEMENT CONTROL
  void Movement()
    {
        if (!Input.GetMouseButton(1))
        {
            //LOOKDIRECTION
            mouseXLook += lookSpeed * Input.GetAxis("Mouse X");
            transform.eulerAngles = new Vector3(0.0f, mouseXLook, 0.0f);
        }

        //MOVEDIRECTION
        moveDirection = (transform.forward*Input.GetAxis("Vertical") * walkSpeed);
        strafeDirection = (transform.right* Input.GetAxisRaw("Horizontal") * strafeSpeed);
        if (IsGrounded())
        {
            verticalSpeed = 0;
            if (Input.GetButton("Jump"))//spacebar
            {
                verticalSpeed = jumpSpeed;

            }
            lastDirection = moveDirection;
        }
        else
        {
           // moveDirection = lastDirection; //stops player changing direction while in midair                    
            strafeDirection =  Vector3.zero;
        }

        verticalSpeed -= gravity * Time.fixedDeltaTime;        //apply constant gravity to smooth the fall zomg looks so reals
        moveDirection.y = verticalSpeed;
        strafeDirection.y = verticalSpeed;
        controller.Move(moveDirection*Time.fixedDeltaTime); //forward/back
        controller.Move(strafeDirection *Time.fixedDeltaTime); //strafe
    }



   bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f); //checks to see if the raycast hits from the bottom of the character hits anything
    }

    //PAUSE/UNPAUSE PLAYER MOVEMENT
    public void Pause()
    {
        pause = true;
    }

    public void Unpause()
    {
        pause = false;
    }



}
