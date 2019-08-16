using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AtivarInimigos : MonoBehaviour {

	public GameObject[] objetoInimigo;
	
	// Use this for initialization
	void OnBecameVisible () {
		for(int i = 0;i < objetoInimigo.Length; i++)
		{
			objetoInimigo[i].SetActive(true);
		}
	}
}
