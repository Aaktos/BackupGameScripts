using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    LedgeController ledgeScript;
    CharacterController controller;
    Animator animator;
    Collider playerCollider;
    Vector3 strafeDirection;
    Vector3 moveDirection;
    Vector3 mousePosition;
    Vector3 lastDirection;
    Vector3 lastStrafeDirection;
    Quaternion lookRotation;

    public float walkSpeed;
    public float strafeSpeed;
    public float jumpSpeed;
    public float gravity;
    public float lookSpeed;
    public bool inAir;

    bool pause; //handles pausing and unpausing off player controls


    float distToGround;
    float verticalSpeed;
    float mouseXLook = 0.0f;


    void Start () {
        playerCollider = GetComponent<Collider>();
        controller = GetComponent<CharacterController>();  
        animator = GetComponent<Animator>();
        ledgeScript = GetComponentInChildren<LedgeController>();

        distToGround = playerCollider.bounds.extents.y;// distance from the centre of the player collider to the bottom/top

        //Pause Subscriptions -- Testing this kind of method of unpausing/ pausing the game for now
        ledgeScript.OnLedgeEvent += Pause;
        ledgeScript.OffLedgeEvent += Unpause;   
    }
	
    void FixedUpdate()
    {   if(!pause)
            Movement();
 
    }

 //MOVEMENT CONTROL
    void Movement()
    {
        //LOOKDIRECTION
        if (!Input.GetMouseButton(1)) // if not in mouse orbit
        {           
            mouseXLook += lookSpeed * Input.GetAxis("Mouse X");
            transform.eulerAngles = new Vector3(0.0f, mouseXLook, 0.0f);
        }

        moveDirection = (transform.forward*Input.GetAxis("Vertical") * walkSpeed);
        strafeDirection = (transform.right* Input.GetAxisRaw("Horizontal") * strafeSpeed);

        if (IsGrounded())
        {
            inAir = false;
            verticalSpeed = 0;
            if (Input.GetButton("Jump"))//spacebar           
                verticalSpeed = jumpSpeed;

            lastDirection = moveDirection;
            lastStrafeDirection = strafeDirection;
        }
        else       
            inAir = true;
        
        //Apply Movement Decisions
        verticalSpeed -= gravity * Time.fixedDeltaTime;   //apply constant gravity to smooth the fall zomg looks so reals
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
        mouseXLook =  transform.eulerAngles.y; //added in to account for movement changes during the pause 
    }



}
