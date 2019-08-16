using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOut : MonoBehaviour {

	private fade fade;

	// Use this for initialization
	void Start () {

		fade = FindObjectOfType (typeof(fade)) as fade;
		fade.fadeOut ();
		
	}
	

}
