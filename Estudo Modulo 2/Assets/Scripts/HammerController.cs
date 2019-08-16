using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour {

	private 	playerController	_PC;
	public		float 				ataqueBaseHammer;
	public		float 				ataqueCalculado;

	// Use this for initialization
	void Start () {
		_PC = FindObjectOfType (typeof(playerController)) as playerController;
		ataqueCalculado = _PC.ataqueBase + ataqueBaseHammer;
	}
	
}
