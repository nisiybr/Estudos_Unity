using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rings : MonoBehaviour {

	private gameController _GC;

	// Use this for initialization
	void Start () {
		_GC = FindObjectOfType(typeof(gameController)) as gameController;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void coletavel(){
		_GC.coletarRings(1,10);
		Destroy(this.gameObject);
	}
}
