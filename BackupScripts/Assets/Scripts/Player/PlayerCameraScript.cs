using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{
    public Transform player;
    Quaternion playerRotation;

    public float cameraHeight;
    public float followDistance;
    public float rotationSpeed;

    Vector3 mainPosition;
    Vector3 mainRotation;

    float mouseXLook; //this is so it starts behind our character
    float mouseYLook;
    float angleX;
    float angleY;
    float radius;

    void Start()
    {
        //  new Vector3(player.transform.localPosition.x, player.transform.localPosition.y - 1f, player.transform.localPosition.z)
        //transform.forward = -1 * player.transform.position.normalized;
        transform.LookAt(player);
        radius = 10f;       
        mouseYLook = transform.localEulerAngles.x;
        mouseXLook =  -90f;
        mainPosition = transform.localPosition;
        mainRotation = transform.localEulerAngles;
    }


    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
            mouseOrbit();
        else if (Input.GetMouseButtonUp(1))
        {
            transform.localPosition = mainPosition;
            transform.localEulerAngles = mainRotation;
            mouseYLook = transform.localEulerAngles.x;
            mouseXLook =  - 90f;
        }

       transform.LookAt(player);
    }

    void mouseOrbit()
    {
        mouseXLook -= Input.GetAxis("Mouse X");
        mouseYLook -= Input.GetAxis("Mouse Y");
      //  print("X" + mouseXLook);
       // print("Y" + mouseYLook);
        angleX = mouseXLook;
        angleX *= Mathf.Deg2Rad;
        angleY = Mathf.Clamp(mouseYLook, -10f, 60f);
        mouseYLook = angleY; // keep the mouseYlook at 5 or 30
        angleY *= Mathf.Deg2Rad;
        transform.localPosition = radius * (new Vector3( Mathf.Cos(angleX), Mathf.Sin(angleY), Mathf.Sin(angleX)));
        
        //what its based off of
        //X= originX + cos(angle) * radius;
        //Y= originY + sin(angle) * radius; (which is Z in our case)
    }

}
