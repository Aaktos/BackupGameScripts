using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{
    public Transform player;

    public float cameraHeight;
    public float followDistance;
    public float rotationSpeed; 

    Vector3 mainPosition;
    Vector3 mainRotation;
    float mouseXLook; 
    float mouseYLook;
    float startAngleX;
    float startAngleY;
    float angleX;
    float angleY;
    float radius;
    float distance;

    int wallLayerMask;
/*******THE CAMERA MUST BE A CHILD OF THE PLAYER AND BE SET AT 0,0,0*******/
    void Start()
    {
        //Mouse Orbit
        transform.localPosition = new Vector3(0f, cameraHeight, -followDistance);
        transform.LookAt(player);
        mainRotation = transform.localEulerAngles; // store the starting rotation
        mainPosition = transform.localPosition;    // and the starting position for snap back
        radius = followDistance; // z axis defines radius      
        startAngleY = mainRotation.x;
        startAngleX =  mainRotation.y - 90f; //XaxisRotation - 90f to correct the starting posistion of the angle( using cos)
        mouseYLook = startAngleY;
        mouseXLook = startAngleX;
        
        wallLayerMask = 1 << 8;
    }

    void FixedUpdate()
    {       
        CameraCast();
    }

    void LateUpdate()
    {
        if (Input.GetButton("RightClick"))
            mouseOrbit();
        else if (Input.GetButtonUp("RightClick"))  //snaps the camera back to place after using mouse orbit
            LookReset();

        distance = Vector3.Distance(transform.position, player.position);
        transform.LookAt(player);
    }

    //MOUSEORBIT
    void mouseOrbit()
    {
        mouseXLook -= (Input.GetAxis("Mouse X")*rotationSpeed);//
        mouseYLook -= (Input.GetAxis("Mouse Y")*rotationSpeed);//
        angleX = mouseXLook;
      //  mouseYLook = Mathf.Clamp(mouseYLook, 0f, 65f); //VERTICAL CLAMP RANGE
        angleY = mouseYLook; // stops mouseYLook from becoming more/less than its clamped value;
        angleX *= Mathf.Deg2Rad;
        angleY *= Mathf.Deg2Rad;
        transform.localPosition = radius * (new Vector3( Mathf.Cos(angleX), Mathf.Tan(angleY), Mathf.Sin(angleX)));
       
        
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



    void CameraCast()
    {
        Ray cameraRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawLine(transform.position, player.position, Color.red);
        if (Physics.Raycast(cameraRay, out hit, distance, wallLayerMask))
        {
            print("Hitting Walls"); 
            Renderer rend = hit.transform.GetComponent<Renderer>();
            if (rend)
            {
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3F;
                rend.material.color = tempColor;
            }
        }
            
        else
            print("I'm looking at nothing!");
    }



    //void Update()
    //{
    //    if (Input.GetKeyDown("f"))
    //    {
    //        StartCoroutine("Fade");
    //    }
    //}

    //IEnumerator Fade()
    //{
    //    for (float f = 1f; f >= 0; f -= 0.1f)
    //    {
    //        Color c = renderer.material.color;
    //        c.a = f;
    //        renderer.material.color = c;
    //        yield return null;
    //    }
    //}
}



