using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{
    Transform player;

    public float radius; //z axis
    public float cameraHeight; // y axis
    public float rotationSpeed;
    public float distance;
    public float offset;
    Vector3 cameraOffset;
    Vector3 mainPosition;
    Vector3 mainRotation;
    Vector3 lastPosition;
    float mouseXLook; 
    float mouseYLook;
    float startAngleX;
    float startAngleY;
    float angleX;
    float angleY;


    /*******THE CAMERA MUST BE A CHILD OF THE PLAYER AND BE SET AT 0,0,0*******/
    void Start()
    {
        player = transform.root; //parent transform
        //Mouse Orbit
        transform.localPosition = new Vector3(0f, cameraHeight, -radius);
        cameraOffset = new Vector3(player.position.x, player.position.y + offset, player.position.z);
        transform.LookAt(cameraOffset);
        mainRotation = transform.localEulerAngles; // store the starting rotation
        mainPosition = transform.localPosition;    // and the starting position for snap back     
        startAngleY = mainRotation.x;
        startAngleX =  mainRotation.y - 90f; //XaxisRotation - 90f to correct the starting posistion of the angle( using cos)
        mouseYLook = startAngleY;
        mouseXLook = startAngleX;

    }


    void LateUpdate()
    {

        if (Input.GetButton("RightClick"))
            mouseOrbit();
        else if (Input.GetButtonUp("RightClick"))  //snaps the camera back to place after using mouse orbit
            LookReset();     

        cameraOffset = new Vector3(player.position.x, player.position.y + offset, player.position.z);
        distance = Vector3.Distance(transform.position, player.position);
        transform.LookAt(cameraOffset);
        

    }

    //MOUSEORBIT
    void mouseOrbit()
    {
        mouseXLook -= (Input.GetAxis("Mouse X")*rotationSpeed);//
        mouseYLook -= (Input.GetAxis("Mouse Y")*rotationSpeed);//
        angleX = mouseXLook;
        print("" + mouseYLook);
        mouseYLook = Mathf.Clamp(mouseYLook, 5f, 55f); //VERTICAL CLAMP RANGE
        angleY = mouseYLook; // stops mouseYLook from becoming more/less than its clamped value;
        angleX *= Mathf.Deg2Rad;
        angleY *= Mathf.Deg2Rad;
        transform.localPosition = radius * (new Vector3( Mathf.Cos(angleX), Mathf.Tan(angleY)+(offset/radius), Mathf.Sin(angleX)));
             
        //what its based off of
        //X= originX + cos(angle) * radius;
        //Y= originY + sin(angle) * radius; (which is Z in our case)
    }

    void LookReset()
    {
        transform.localPosition = mainPosition;
        mouseYLook = startAngleY;
        mouseXLook = startAngleX;
    }
}



