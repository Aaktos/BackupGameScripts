using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))] //adds character controller to gameobject using this script
[RequireComponent(typeof(PlayerControl))] //uses playerController script
public class Player : MonoBehaviour {

    PlayerControl playerControl;
    Camera mainCamera;
    

	void Start () {
        playerControl = GetComponent<PlayerControl>();
        mainCamera = Camera.main;
        
	}


    void Update()
    {              
        Ray cursorRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        MouseOver(cursorRay);

     }

    public void MouseOver(Ray cursorRay) //MOUSE CONTROL
    {
        //MOUSE POSITION
        float rayDistance;
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); //imaginary-ish plane

        if (groundPlane.Raycast(cursorRay, out rayDistance))
        {
            
            Vector3 mousePoint = cursorRay.GetPoint(rayDistance);
            playerControl.MousePosition(mousePoint); //send mouse position to player control script to be used for look direction
        }
        
    }
}



