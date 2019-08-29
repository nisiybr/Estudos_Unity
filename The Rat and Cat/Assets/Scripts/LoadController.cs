using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadController : MonoBehaviour {

	private	AudioController	_AC;
	private	TransitionControllerGamePlay _TC;
	private bool verificado;


	// Use this for initialization
	void Start () {
		_AC = FindObjectOfType (typeof(AudioController)) as AudioController;
		_TC = FindObjectOfType (typeof(TransitionControllerGamePlay)) as TransitionControllerGamePlay;


	}
	
	// Update is called once per frame
	void Update () {
		if (!verificado && !_AC.SourceMusic.isPlaying) {
			verificado = true;
			_TC.cenaPara = "gamePlay";
			_AC.StartCoroutine ("changeMusic", _AC.mGrass);
			_TC.callFadeOut ();

		} else {
			_AC.SourceMusic.loop = false;
		}
	}
}
