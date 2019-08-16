using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour {

	private 	playerController	_PC;
	public		float 				ataqueBaseBall;
	public		float 				ataqueCalculado;

	// Use this for initialization
	void Start () {
		_PC = FindObjectOfType (typeof(playerController)) as playerController;
		ataqueCalculado = _PC.ataqueBase + ataqueBaseBall;
		Destroy (this.gameObject, 5f);
	}
}
