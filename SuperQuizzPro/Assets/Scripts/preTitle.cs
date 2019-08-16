using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class preTitle : MonoBehaviour {

	public int tempoEspera;

	private fade fade;

	// Use this for initialization
	void Start () {
		StartCoroutine ("esperaTempo");
		fade = FindObjectOfType (typeof(fade)) as fade;
	}

	
	IEnumerator esperaTempo()
	{
		yield return new WaitForSeconds (tempoEspera);
		fade.fadeIn ();

		yield return new WaitWhile(()  => fade.fume.color.a < 0.9f );
		SceneManager.LoadScene ("title");
	}

}
