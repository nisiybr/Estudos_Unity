using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour {
	public string cena;
	public Animator anim;
	// Use this for initialization

	public void CallFadeout()
	{
		anim.SetTrigger ("FadeOut");
	}
	
	// Update is called once per frame
	void OnFadeComplete () {
		SceneManager.LoadScene(cena);
	}
}
