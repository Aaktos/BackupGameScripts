using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    CharacterController controller;
    Collider playerCollider;
    Vector3 strafeDirection;
    Vector3 moveDirection;
    Vector3 lookDirection;
    Vector3 mousePosition;
    Vector3 lastDirection;

    public float walkSpeed;
    public float strafeSpeed;
    public float jumpSpeed;
    public float jumpReduceMove;
    public float gravity;
    public bool pause; //handles pausing and unpausing off player controls

    float distToGround;
    float verticalSpeed;

	void Start () {
        playerCollider = GetComponent<Collider>();
        controller = GetComponent<CharacterController>();

        distToGround = playerCollider.bounds.extents.y;// distance from the centre of the player collider to the bottom/top
    }
	
    void FixedUpdate()
    {
         Movement();
    }

     //MOVEMENT CONTROL
  void Movement()
    {
       
        transform.LookAt(lookDirection);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0)); //keeps the character on the level
        moveDirection = (transform.forward * Input.GetAxis("Vertical") * walkSpeed);
        strafeDirection = (transform.right * Input.GetAxisRaw("Horizontal") * strafeSpeed);
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
            moveDirection = lastDirection; //stops player changing direction while in midair                    
            strafeDirection =  Vector3.zero;
        }

        verticalSpeed -= gravity * Time.fixedDeltaTime;        //apply constant gravity to smooth the fall zomg looks so reals
        moveDirection.y = verticalSpeed;
        strafeDirection.y = verticalSpeed;
        controller.Move(moveDirection*Time.fixedDeltaTime); //forward/back
        controller.Move(strafeDirection *Time.fixedDeltaTime); //strafe
    }

    public void MousePosition(Vector3 mousePoint) //recieves mouse position
    {
        mousePosition = mousePoint;
        lookDirection = new Vector3(mousePosition.x, 1f, mousePosition.z); //the 1 is so the head keeps level, still a little buggy up close        
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
