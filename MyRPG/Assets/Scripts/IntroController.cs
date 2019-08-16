using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour {

	private TransitionController _TC;

	// Use this for initialization
	void Start () {
		_TC = FindObjectOfType (typeof(TransitionController)) as TransitionController;
		StartCoroutine ("ChamaFuncaoTransition");
	}
	
	IEnumerator ChamaFuncaoTransition()
	{
		yield return new WaitForSeconds (3f);
		_TC.cena = "gamePlay";
		_TC.CallFadeout ();
	}
}
