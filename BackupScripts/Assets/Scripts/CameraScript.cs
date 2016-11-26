using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public Transform player;
    public float cameraHeight;
	// Use this for initialization
	void Start () {
        transform.eulerAngles = new Vector3(35, 45, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x - 20f, cameraHeight, player.position.z -20f); //this is super specific to camera rotation atm, use 35x, 45y, 0z and ~ 10 or 15 for size
	}
}
