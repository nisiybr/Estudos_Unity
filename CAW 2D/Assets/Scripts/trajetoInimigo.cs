using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajetoInimigo : MonoBehaviour {

	public Transform naveInimiga;
	public Transform[] checkpoints;
	public float velocidadeMovimento;
	public float[] delayParado;
	
	private int idCheckPoints;
	private bool movimentar;

	// Use this for initialization
	void Start () {
		naveInimiga.transform.position = checkpoints[0].position;
		StartCoroutine("iniciaMovimentoInimigo");
	}
	
	// Update is called once per frame
	void Update () {
		if(movimentar)
		{
			naveInimiga.transform.position = Vector3.MoveTowards(naveInimiga.transform.position,checkpoints[idCheckPoints].position,velocidadeMovimento * Time.deltaTime);
			if(naveInimiga.transform.position == checkpoints[idCheckPoints].position)
			{
				movimentar = false;
				StartCoroutine("iniciaMovimentoInimigo");
			}
		}
	}

	IEnumerator iniciaMovimentoInimigo()
	{
		idCheckPoints += 1;
		if(idCheckPoints >= checkpoints.Length)
		{
			idCheckPoints = 0;
		}
		yield return new WaitForSeconds(Random.Range(delayParado[0],delayParado[1]));
		movimentar = true;
	}
}
