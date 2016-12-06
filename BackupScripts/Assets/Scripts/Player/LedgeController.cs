using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeController : MonoBehaviour {
    Transform player;
    Transform ledge;
    GameObject playerObj;
    CharacterController controller;

    float startTime;
    float lerpTime;
    float rotatePlayer;

    bool onLedge; // initial ledge hang to allow ledge controls to be available
    bool climbing;// climb up ledge


  public System.Action OnLedgeEvent; //freeze normal control delegate
  public System.Action OffLedgeEvent; // return normal control delegate

    void Start ()
    {

        player = transform.root;
        controller = player.GetComponent<CharacterController>();
	}

    private void Update()
    {
        if (onLedge)
            OnLedgeControls();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ledges"))
        {
            OnLedgeEvent();
            ledge = other.gameObject.transform;
            rotatePlayer = player.eulerAngles.y + (Mathf.DeltaAngle(player.eulerAngles.y, ledge.eulerAngles.y)); //shortest angle to face + the players on contact
            if(rotatePlayer >= 360f)           
                rotatePlayer = 359.9f; // bleh, short term fix to a bug caused at the 360 degree mark, this feels like dirty code but it works really well for now. cleaner solution wanted.
                
            onLedge = true;
            startTime = Time.time; //handle das lerping
            lerpTime = 0f;
        }
    }

    private void OnLedgeControls()
    {
        if (lerpTime <= 1f)
        {
            lerpTime = (Time.time - startTime);
            player.eulerAngles = Vector3.Lerp(player.eulerAngles, (new Vector3(player.eulerAngles.x, rotatePlayer, player.eulerAngles.z)), lerpTime);
        }

        //Controls
        if (Input.GetButtonDown("Jump") && (Input.GetAxis("Vertical") > 0)) //climbup
        {
            onLedge = false;
            climbing = true;          
            StartCoroutine(ClimbLedge());
        }
        else if (Input.GetButtonDown("Jump") && (Input.GetAxis("Vertical") < 0)) //hopoff
        {
            onLedge = false;
            OffLedgeEvent();
        }
        //else if (Input.GetAxis("Horizontal") != 0)
        //{
        //    player.position += (transform.right * Input.GetAxis("Horizontal")/20);
        //}
    }

    private void LedgeMovement()
    {

    }

    IEnumerator ClimbLedge()
    {
        float stepPercent = 0f, climbPercent = 0f;
        float targetHeight = (player.position.y + ((controller.bounds.extents.y) * 2.1f));
        float time = Time.time;
        bool stepForward = false;
        Vector3 startPos = player.position;

        while (climbing)
        {
            climbPercent = (Time.time - time);
            player.position = Vector3.Lerp(startPos, (new Vector3(player.position.x, targetHeight, player.position.z)), climbPercent);
            if (climbPercent >= 1)
            {
                climbing = false;
                stepForward = true;
            }
            yield return null;
        }

        startPos = player.position;
        Vector3 endPos = startPos + (transform.forward);
        time = Time.time;

        while (stepForward)
        {
            player.position = Vector3.Lerp(startPos, endPos, stepPercent);
            stepPercent = 2*(Time.time - time);
            if (stepPercent >= 1)
                stepForward = false;
            yield return null;
        }
             
        OffLedgeEvent();
    }



}
