using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direcao
{
	cima,baixo,esquerda,direita
}

public class aulaEnum : MonoBehaviour {

	public direcao direcaoMovimento;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		switch(direcaoMovimento)
		{
		case direcao.baixo: print("baixo");break;
		case direcao.cima: print("cima");break;
		case direcao.direita: print("direita");break;
		case direcao.esquerda: print("esquerda");break;
		}
		
	}
}
