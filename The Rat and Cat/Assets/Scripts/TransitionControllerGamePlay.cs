using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControllerGamePlay : MonoBehaviour {
	public Animator anim;
	public string cenaPara;

	// Use this for initialization


	public void callFadeOut()
	{
		anim.SetTrigger ("FadeOut");				
	}
	void OnFadeComplete()
	{
			SceneManager.LoadScene (cenaPara);
	}
}
