using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWay : MonoBehaviour {
	
	private playerController _PC;
	private BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {
		_PC				= FindObjectOfType(typeof(playerController)) as playerController;
		boxCollider		= GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_PC.transform.position.y < transform.position.y)
		{
			boxCollider.enabled = false;
		} else {
			boxCollider.enabled = true;
		}
	}
}
