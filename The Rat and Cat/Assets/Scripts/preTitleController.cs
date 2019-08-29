using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preTitleController : MonoBehaviour {

	private	TransitionControllerGamePlay _TC;



	// Use this for initialization
	void Start () {

		_TC = FindObjectOfType (typeof(TransitionControllerGamePlay)) as TransitionControllerGamePlay;
		StartCoroutine ("preTitle");
	}
		
	IEnumerator preTitle()
	{
		yield return new WaitForSeconds (3f);
		_TC.cenaPara = "Menu";
		_TC.callFadeOut ();
	}
}
