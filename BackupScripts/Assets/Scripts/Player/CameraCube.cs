using UnityEngine;
using System.Collections;

public class CameraCube : MonoBehaviour {

    Transform player;
    PlayerCameraScript cameraScript;
    float length;


	void Start () {
        player = transform.root;
        cameraScript = transform.parent.GetComponent<PlayerCameraScript>();
	}
	

	void LateUpdate ()
    {
        length = cameraScript.distance;
        transform.localScale = (new Vector3(1, 1, (length-2f)*2));
        transform.LookAt(player);
	}

}
