using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour {
	private AudioController _AC;

	public Animator anim;
	public string cenaPara;
	// Use this for initialization
	void Start () {
		_AC = FindObjectOfType (typeof(AudioController)) as AudioController;
		cenaPara = "loading";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) {
			print ("Voila!");
			_AC.StartCoroutine("changeMusic",_AC.mStart);
			anim.SetTrigger ("FadeOut");				
		}
	}

	void OnFadeComplete()
	{
		SceneManager.LoadScene(cenaPara);
	}
}
