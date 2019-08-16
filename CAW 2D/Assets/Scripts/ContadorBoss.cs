using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorBoss : MonoBehaviour {

	private GameController _GC;

	// Use this for initialization
	void Start () {
		_GC = FindObjectOfType(typeof(GameController)) as GameController;
	}
	void OnBecameInvisible()
	{
		_GC.qtdBoss++;
		Destroy(this.gameObject);
	}

}
