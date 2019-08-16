using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private 	playerController	_PC;

	// Use this for initialization
	void Start () {
		_PC = FindObjectOfType (typeof(playerController)) as playerController;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (_PC.transform.position.x, transform.position.y, transform.position.z);
	}
}
