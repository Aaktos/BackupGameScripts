using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))] //adds character controller to gameobject using this script
[RequireComponent(typeof(PlayerControl))] //uses playerController script
public class PlayerScript : MonoBehaviour {

    PlayerControl playerControl;
    
	void Start () {
        playerControl = GetComponent<PlayerControl>();        
	}


}



