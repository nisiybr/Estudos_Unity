using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregaScene : MonoBehaviour {

	private TransitionController _TC;

	void Start()
	{
		_TC = FindObjectOfType (typeof(TransitionController)) as TransitionController;
	}

	// Use this for initialization
	public void carregaScene(string nomeScene)
	{
		_TC.cena = nomeScene;
		_TC.CallFadeout();
	}
}
