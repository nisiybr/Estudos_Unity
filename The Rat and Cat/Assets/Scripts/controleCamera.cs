using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleCamera : MonoBehaviour {
		
	private playerController 	_PC;
	public 	Transform			leftLimitCamera;
	public 	Transform			rightLimitCamera;	


	// Use this for initialization
	void Start () {
		_PC				= FindObjectOfType(typeof(playerController)) as playerController;
		transform.position = new Vector3(leftLimitCamera.transform.position.x,transform.position.y,transform.position.z);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_PC.transform.position.x > leftLimitCamera.transform.position.x && _PC.transform.position.x < rightLimitCamera.transform.position.x)
		{
			transform.position = new Vector3(_PC.transform.position.x,transform.position.y,transform.position.z);
		}
		/*if()
		{
			transform.position = new Vector3(_PC.transform.position.x,transform.position.y,transform.position.z);
		}	*/	
	}
}
