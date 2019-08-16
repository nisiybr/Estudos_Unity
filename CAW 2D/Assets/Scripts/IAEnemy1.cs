using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemy1 : MonoBehaviour {
	public float velocidadeMovimento;
	public float pontoInicialCurva;
	public float grausCurva;
	private bool isCurva;
	public float incrementar;
	private float incrementado;
	private float rotacaoZ;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y <= pontoInicialCurva && !isCurva)
		{
			isCurva = true;
		}
		if(isCurva && incrementado < grausCurva)
		{
			rotacaoZ += incrementar;
			transform.rotation = Quaternion.Euler(0,0,rotacaoZ);
			
			if(incrementar <0)
			{
				incrementado += (incrementar *= 1);
			} else 
			{
				incrementado += incrementar;	
			}
		}
		transform.Translate(Vector3.down * velocidadeMovimento * Time.deltaTime);
	}
}
