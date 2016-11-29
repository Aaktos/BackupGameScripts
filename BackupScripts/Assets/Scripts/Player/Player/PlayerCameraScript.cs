using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour
{
    Transform player;

    public float cameraHeight;
    public float followDistance;
    public float rotationSpeed;
    public float distance;

    Vector3 mainPosition;
    Vector3 mainRotation;
    float mouseXLook; 
    float mouseYLook;
    float startAngleX;
    float startAngleY;
    float angleX;
    float angleY;
    float radius;
 

    int wallLayerMask;


/*******THE CAMERA MUST BE A CHILD OF THE PLAYER AND BE SET AT 0,0,0*******/
    void Start()
    {
        player = transform.root; //parent transform
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
        //CameraCast();
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


    //CAMERARAYCAST
  //  void CameraCast()
  //  {
  //      RaycastHit[] hits;
  //      hits = Physics.RaycastAll(transform.position, transform.forward, distance, wallLayerMask);

  //      for (int i = 0; i < hits.Length; i++)
  //      {
  //          RaycastHit hit = hits[i];
  //          Renderer rend = hit.transform.GetComponent<Renderer>();

  //          if (rend)            
  //              StartCoroutine(Fade(rend));          
  //      }
  //  }

  //IEnumerator Fade(Renderer rend)
  //  {
  //      float fadePercent = 1f;
  //      float fadeTime = 3f;
  //      rend.material.shader = Shader.Find("Transparent/Diffuse");
  //      Color tempColor = rend.material.color;
  //      while (fadePercent >= 0.3f)
  //      {
  //          fadePercent -= (0.1f * Time.deltaTime*fadeTime);
  //          tempColor.a = fadePercent;
  //          rend.material.color = tempColor;
  //          yield return null;
  //      }  
  //  }
}



